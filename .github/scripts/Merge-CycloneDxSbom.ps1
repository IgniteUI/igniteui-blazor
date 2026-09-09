<#
.SYNOPSIS
    Merges the .NET and npm CycloneDX SBOMs into the single document that gets checksummed and attested.

.DESCRIPTION
    There is no dependency-manager-distributed tool that merges two already-generated CycloneDX
    documents: cyclonedx-cli (the only tool with a merge command) ships as GitHub-release binaries only,
    and @cyclonedx/cyclonedx-library can serialize model objects to JSON but cannot deserialize existing
    CycloneDX JSON back into models, so it can't load two finished documents to combine them either.
    This performs the merge directly against the JSON structure instead of depending on either.

    The merge is hierarchical: a new top-level component identifies the published package, each input
    document's own metadata.component becomes a direct child of it (added to the flat 'components' array,
    linked via 'dependencies', not nested inside 'components[].components' - CycloneDX's own convention
    for expressing "these are all the parts, here is how they relate" is the dependency graph, not
    structural nesting), and each input's own components/dependencies are carried over unchanged. That
    keeps which ecosystem a component came from traceable in the dependency graph, instead of flattening
    both into one undifferentiated list with no record of provenance.

    Metadata 'tools' entries are deliberately dropped rather than merged: the array-vs-object shape of
    that field changed across CycloneDX spec versions, and getting it wrong risks a malformed document
    for a field that carries no information this fix needs.
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

    [string]$SpecVersion = '1.6'
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
        throw "$Label document has no metadata.component; cannot nest it under the merged root."
    }
    if (-not $bom.metadata.component.'bom-ref') {
        throw "$Label document's metadata.component has no bom-ref; cannot link it into the merged dependency graph."
    }

    $bom
}

$dotnetBom = Import-CycloneDxBom -Path $DotNetBomPath -Label '.NET'
$npmBom = Import-CycloneDxBom -Path $NpmBomPath -Label 'npm'

$rootBomRef = "root-$([guid]::NewGuid())"
$rootComponent = [ordered]@{
    type      = 'library'
    'bom-ref' = $rootBomRef
    group     = $Group
    name      = $PackageId
    version   = $PackageVersion
}

$mergedComponents = [System.Collections.Generic.List[object]]::new()
$mergedDependencies = [System.Collections.Generic.List[object]]::new()
$childBomRefs = [System.Collections.Generic.List[string]]::new()

foreach ($side in @($dotnetBom, $npmBom)) {
    $mergedComponents.Add($side.metadata.component)
    $childBomRefs.Add($side.metadata.component.'bom-ref')

    foreach ($component in @($side.components)) {
        $mergedComponents.Add($component)
    }
    foreach ($dependency in @($side.dependencies)) {
        $mergedDependencies.Add($dependency)
    }
}

$mergedDependencies.Add([ordered]@{ ref = $rootBomRef; dependsOn = @($childBomRefs) })

$merged = [ordered]@{
    bomFormat    = 'CycloneDX'
    specVersion  = $SpecVersion
    serialNumber = "urn:uuid:$([guid]::NewGuid())"
    version      = 1
    metadata     = [ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ssZ')
        component = $rootComponent
    }
    components   = $mergedComponents
    dependencies = $mergedDependencies
}

$outputDirectory = Split-Path -Path $OutputFile -Parent
if ($outputDirectory) {
    New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
}

$merged | ConvertTo-Json -Depth 100 | Set-Content -LiteralPath $OutputFile -Encoding utf8

Write-Host "Merged .NET ($($dotnetBom.components.Count + 1) components) and npm ($($npmBom.components.Count + 1) components) CycloneDX documents into $OutputFile."
