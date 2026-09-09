<#
.SYNOPSIS
    Merges the .NET and npm CycloneDX SBOMs into the single document that gets checksummed and attested.

.DESCRIPTION
    There is no dependency-manager-distributed tool that merges two already-generated CycloneDX
    documents: cyclonedx-cli (the only tool with a merge command) ships as GitHub-release binaries only,
    and @cyclonedx/cyclonedx-library can serialize model objects to JSON but cannot deserialize existing
    CycloneDX JSON back into models, so it can't load two finished documents to combine them either.
    This performs the merge directly against the JSON structure instead of depending on either.

    One synthetic top-level component identifies the published package. Each input's own components and
    dependency edges are carried over as-is; the two inputs' metadata.component sub-roots are NOT kept
    as components - the merged root's single dependency edge points straight at each ecosystem's
    first-level dependencies, so the graph has exactly one root and no placeholder nodes. The tool
    inventory (metadata.tools.components) of both inputs is preserved and this script is appended to it.
    The root's own licence is set from -RootLicenseExpression and never inherited from an input.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$DotNetBomPath,

    [Parameter(Mandatory)]
    [string]$NpmBomPath,

    [Parameter(Mandatory)]
    [string]$PackageId,

    [Parameter(Mandatory)]
    [string]$PackageVersion,

    [Parameter(Mandatory)]
    [string]$OutputFile,

    [string]$Group = 'Infragistics',

    [string]$SpecVersion = '1.6',

    # SPDX licence expression for the published package itself. Empty omits the licenses node
    # (leaving the package's licence unasserted). Never taken from an input document - cyclonedx-npm
    # reports the repo-root package.json licence for its own metadata.component, which is not this
    # package's licence.
    [string]$RootLicenseExpression = 'MIT',

    # Recorded as the version of this merge step in metadata.tools.components; pass the release SHA.
    [string]$MergeToolVersion
)

$ErrorActionPreference = 'Stop'

function Import-CycloneDxBom {
    param(
        [Parameter(Mandatory)]
        [string]$Path,

        [Parameter(Mandatory)]
        [string]$Label
    )

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        throw "Required file not found: $Path"
    }

    $bom = Get-Content -LiteralPath $Path -Raw | ConvertFrom-Json -Depth 100
    if (-not $bom.metadata -or -not $bom.metadata.component) {
        throw "$Label document has no metadata.component; cannot identify its dependency sub-root."
    }
    if (-not $bom.metadata.component.'bom-ref') {
        throw "$Label document's metadata.component has no bom-ref; cannot resolve its first-level dependencies."
    }

    $bom
}

function Get-ComponentKey {
    param([Parameter(Mandatory)][object]$Component)

    if ($Component.'bom-ref') { return "ref:$($Component.'bom-ref')" }
    if ($Component.purl) { return "purl:$(([string]$Component.purl).ToLowerInvariant())" }
    return "nv:$(([string]$Component.name).ToLowerInvariant())@$([string]$Component.version)"
}

function Get-ToolComponents {
    param($Tools)

    if (-not $Tools) { return @() }

    # Spec 1.4 and earlier: metadata.tools is an array of { vendor, name, version }.
    if ($Tools -is [System.Collections.IEnumerable] -and $Tools -isnot [string]) {
        return @($Tools | Where-Object { $_ } | ForEach-Object {
                $entry = [ordered]@{ type = 'application' }
                if ($_.vendor) { $entry.group = $_.vendor }
                if ($_.name) { $entry.name = $_.name }
                if ($_.version) { $entry.version = $_.version }
                if ($_.externalReferences) { $entry.externalReferences = $_.externalReferences }
                [pscustomobject]$entry
            })
    }

    # Spec 1.5+: metadata.tools = { components: [ ... ], services: [ ... ] }.
    if ($Tools.PSObject.Properties.Name -contains 'components') {
        return @($Tools.components)
    }

    return @()
}

$dotnetBom = Import-CycloneDxBom -Path $DotNetBomPath -Label '.NET'
$npmBom = Import-CycloneDxBom -Path $NpmBomPath -Label 'npm'

$rootBomRef = "root-$([guid]::NewGuid())"
$subRootRefs = @($dotnetBom.metadata.component.'bom-ref', $npmBom.metadata.component.'bom-ref')

# --- components: union of both inputs' own components, deduped; the sub-roots are not components ---
$seenComponentKeys = [System.Collections.Generic.HashSet[string]]::new()
$mergedComponents = [System.Collections.Generic.List[object]]::new()
foreach ($side in @($dotnetBom, $npmBom)) {
    foreach ($component in @($side.components)) {
        if (-not $component) { continue }
        if ($seenComponentKeys.Add((Get-ComponentKey -Component $component))) {
            $mergedComponents.Add($component)
        }
    }
}

# --- dependencies: carry every edge except each input's own sub-root entry, whose dependsOn is
#     folded into the single merged-root edge. Residual references to a sub-root are repointed. ---
$firstLevelDependsOn = [System.Collections.Generic.List[string]]::new()
$mergedDependencies = [System.Collections.Generic.List[object]]::new()

foreach ($side in @($dotnetBom, $npmBom)) {
    $subRootRef = $side.metadata.component.'bom-ref'

    foreach ($dependency in @($side.dependencies)) {
        if (-not $dependency) { continue }

        if ($dependency.ref -eq $subRootRef) {
            foreach ($dep in @($dependency.dependsOn)) {
                if ($dep -and $subRootRefs -notcontains $dep) { $firstLevelDependsOn.Add([string]$dep) }
            }
            continue
        }

        $dependsOn = @(
            @($dependency.dependsOn) |
                Where-Object { $_ } |
                ForEach-Object { if ($subRootRefs -contains $_) { $rootBomRef } else { [string]$_ } } |
                Select-Object -Unique
        )
        $mergedDependencies.Add([ordered]@{ ref = [string]$dependency.ref; dependsOn = $dependsOn })
    }
}

$mergedDependencies.Add([ordered]@{
        ref       = $rootBomRef
        dependsOn = @($firstLevelDependsOn | Select-Object -Unique)
    })

# --- metadata.tools.components: both inputs' inventories plus this merge step ---
$mergeToolEntry = [ordered]@{ type = 'application'; group = $Group; name = 'Merge-CycloneDxSbom.ps1' }
if ($MergeToolVersion) { $mergeToolEntry.version = $MergeToolVersion }

$seenToolKeys = [System.Collections.Generic.HashSet[string]]::new()
$mergedTools = [System.Collections.Generic.List[object]]::new()
foreach ($tool in @(Get-ToolComponents -Tools $dotnetBom.metadata.tools) +
    @(Get-ToolComponents -Tools $npmBom.metadata.tools) +
    @([pscustomobject]$mergeToolEntry)) {
    if (-not $tool) { continue }
    $key = "$(([string]$tool.group).ToLowerInvariant())|$(([string]$tool.name).ToLowerInvariant())|$([string]$tool.version)"
    if ($seenToolKeys.Add($key)) { $mergedTools.Add($tool) }
}

# --- synthetic root component ---
$rootComponent = [ordered]@{
    type      = 'library'
    'bom-ref' = $rootBomRef
    group     = $Group
    name      = $PackageId
    version   = $PackageVersion
    purl      = "pkg:nuget/$PackageId@$PackageVersion"
}
if ($RootLicenseExpression) {
    $rootComponent.licenses = @(@{ license = [ordered]@{ id = $RootLicenseExpression } })
}

$merged = [ordered]@{
    bomFormat    = 'CycloneDX'
    specVersion  = $SpecVersion
    serialNumber = "urn:uuid:$([guid]::NewGuid())"
    version      = 1
    metadata     = [ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
        tools     = [ordered]@{ components = @($mergedTools) }
        component = $rootComponent
    }
    components   = @($mergedComponents)
    dependencies = @($mergedDependencies)
}

$outputDirectory = Split-Path -Path $OutputFile -Parent
if ($outputDirectory) {
    New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
}

$merged | ConvertTo-Json -Depth 100 | Set-Content -LiteralPath $OutputFile -Encoding utf8

Write-Host "Merged $(@($dotnetBom.components).Count) .NET and $(@($npmBom.components).Count) npm components into $($mergedComponents.Count) deduped ($($mergedDependencies.Count) dependency nodes, $($mergedTools.Count) tools) at $OutputFile."
