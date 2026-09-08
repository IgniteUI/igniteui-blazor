<#
.SYNOPSIS
    Generates SPDX 2.2 and SPDX 3.0 SBOMs for a packed NuGet package with the pinned sbom-tool.

.DESCRIPTION
    Both formats come out of a single sbom-tool invocation. Generating them separately made the two
    documents disagree: each invocation performed its own ClearlyDefined lookup, so one document could
    carry licenses while the other carried none, and the second scan detected the first document's
    manifest as a component of the build.

    sbom-tool degrades silently when ClearlyDefined is unreachable - it logs a warning, writes
    NOASSERTION licenses and still exits 0. ClearlyDefined harvests package definitions on demand, so a
    coordinate it has not seen before can stall until the gateway times out, while the same request
    succeeds once the definition is cached. Generation is therefore retried while coverage improves.

.OUTPUTS
    Manifests are written to $OutputRoot/_manifest/spdx_2.2 and $OutputRoot/_manifest/spdx_3.0.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackageId,

    [Parameter(Mandatory)]
    [string]$PackageVersion,

    # -b: files under this path are hashed into the SBOM's files section.
    [Parameter(Mandatory)]
    [string]$BuildDropPath,

    # -bc: root the component detectors scan to build the dependency graph.
    [Parameter(Mandatory)]
    [string]$BuildComponentPath,

    # Must sit outside BuildComponentPath, or the generated manifests become components of the build.
    [Parameter(Mandatory)]
    [string]$OutputRoot,

    [string]$Supplier = 'Infragistics Inc.',

    [string]$NamespaceBaseUri,

    # External license lookup is a network call per component; bound it or turn it off.
    [bool]$ResolveLicenses = $true,

    [int]$LicenseTimeoutSeconds = 180,

    [ValidateRange(1, 5)]
    [int]$MaxAttempts = 3,

    [int]$RetryDelaySeconds = 15
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if (-not $NamespaceBaseUri) {
    $NamespaceBaseUri = "http://spdx.org/spdxdocs/$PackageId"
}

$resolvedOutputRoot = [System.IO.Path]::GetFullPath($OutputRoot)
$resolvedComponentPath = [System.IO.Path]::GetFullPath($BuildComponentPath)

function Test-PathIsWithin {
    param(
        [Parameter(Mandatory)]
        [string]$Path,

        [Parameter(Mandatory)]
        [string]$PotentialParent
    )

    # Compares directory segments rather than a raw string prefix, so a sibling directory whose name
    # merely starts with the same characters (e.g. 'repo-output' next to 'repo') is not flagged as nested.
    [char[]]$separators = [System.IO.Path]::DirectorySeparatorChar, [System.IO.Path]::AltDirectorySeparatorChar
    $pathSegments = $Path.Split($separators, [System.StringSplitOptions]::RemoveEmptyEntries)
    $parentSegments = $PotentialParent.Split($separators, [System.StringSplitOptions]::RemoveEmptyEntries)

    if ($pathSegments.Count -lt $parentSegments.Count) {
        return $false
    }

    for ($i = 0; $i -lt $parentSegments.Count; $i++) {
        # PowerShell string comparison operators are case-insensitive by default.
        if ($pathSegments[$i] -ne $parentSegments[$i]) {
            return $false
        }
    }

    return $true
}

if (Test-PathIsWithin -Path $resolvedOutputRoot -PotentialParent $resolvedComponentPath) {
    throw "OutputRoot '$resolvedOutputRoot' is inside BuildComponentPath '$resolvedComponentPath'. The generated manifests would be scanned as components of the build."
}

$spdx22Manifest = Join-Path $resolvedOutputRoot '_manifest/spdx_2.2/manifest.spdx.json'

function Get-LicenseCoverage {
    param([string]$ManifestPath)

    if (-not (Test-Path -LiteralPath $ManifestPath -PathType Leaf)) {
        return [pscustomobject]@{ Total = 0; Licensed = 0 }
    }

    $packages = @((Get-Content -LiteralPath $ManifestPath -Raw | ConvertFrom-Json).packages)

    [pscustomobject]@{
        Total    = $packages.Count
        Licensed = @($packages | Where-Object { $_.licenseConcluded -and $_.licenseConcluded -ne 'NOASSERTION' }).Count
    }
}

$best = [pscustomobject]@{ Total = 0; Licensed = -1 }
$staging = "$resolvedOutputRoot.attempt"

for ($attempt = 1; $attempt -le $MaxAttempts; $attempt++) {
    # Each attempt is generated aside and only promoted if it improves on the one already kept, so a
    # degraded retry can never replace a better document. sbom-tool also will not create -m itself.
    Remove-Item -LiteralPath $staging -Recurse -Force -ErrorAction SilentlyContinue
    New-Item -ItemType Directory -Path $staging -Force | Out-Null

    Write-Host "Generating SPDX 2.2 and SPDX 3.0 SBOMs (attempt $attempt of $MaxAttempts)..."

    # /Verbosity has to precede the remaining switches or the parser binds its value to another argument.
    dotnet tool run sbom-tool -- generate `
        /Verbosity:Information `
        -b $BuildDropPath `
        -bc $BuildComponentPath `
        -m $staging `
        -pn $PackageId `
        -pv $PackageVersion `
        -ps $Supplier `
        -nsb $NamespaceBaseUri `
        -mi 'SPDX:2.2,SPDX:3.0' `
        -li $ResolveLicenses.ToString().ToLowerInvariant() `
        -lto $LicenseTimeoutSeconds `
        -pm true

    $coverage = Get-LicenseCoverage -ManifestPath (Join-Path $staging '_manifest/spdx_2.2/manifest.spdx.json')
    Write-Host "Attempt ${attempt}: $($coverage.Licensed) of $($coverage.Total) packages carry a resolved license."

    if ($coverage.Licensed -gt $best.Licensed) {
        Remove-Item -LiteralPath $resolvedOutputRoot -Recurse -Force -ErrorAction SilentlyContinue
        Move-Item -LiteralPath $staging -Destination $resolvedOutputRoot -Force
        $best = $coverage
    }
    else {
        Remove-Item -LiteralPath $staging -Recurse -Force -ErrorAction SilentlyContinue
        # Zero is an outage rather than a plateau: a coordinate ClearlyDefined has never harvested only
        # becomes available after the request that triggered the harvest has already failed.
        if ($coverage.Licensed -gt 0) {
            Write-Host 'License coverage stopped improving; keeping the document already generated.'
            break
        }
    }

    if (-not $ResolveLicenses -or ($best.Total -gt 0 -and $best.Licensed -eq $best.Total)) {
        break
    }

    if ($attempt -lt $MaxAttempts) {
        Write-Host "Retrying in $RetryDelaySeconds seconds to let ClearlyDefined harvest the missing definitions..."
        Start-Sleep -Seconds $RetryDelaySeconds
    }
}

if (-not (Test-Path -LiteralPath $spdx22Manifest -PathType Leaf)) {
    throw "sbom-tool produced no SPDX 2.2 document at $spdx22Manifest."
}

if ($ResolveLicenses -and $best.Licensed -lt $best.Total) {
    Write-Warning "$($best.Total - $best.Licensed) of $($best.Total) packages have no resolved license and are recorded as NOASSERTION."
}
