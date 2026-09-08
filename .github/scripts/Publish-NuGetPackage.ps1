<#
.SYNOPSIS
    Pushes the signed package to NuGet.org, refusing to overwrite a version that is already published.

.DESCRIPTION
    Deliberately not --skip-duplicate: a rerun produces newly signed bytes, so skipping the duplicate
    would attach this run's SBOM, attestations and checksum to a release whose published package is a
    different build. The API key is sourced from NUGET_API_KEY rather than embedded in the workflow command.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    [Parameter(Mandatory)]
    [string]$PackageId,

    [Parameter(Mandatory)]
    [string]$Version,

    [string]$Source = 'https://api.nuget.org/v3/index.json',

    [string]$FlatContainerBaseUrl = 'https://api.nuget.org/v3-flatcontainer'
)

$ErrorActionPreference = 'Stop'
# Pinned: a failed push is inspected through $LASTEXITCODE below rather than terminating the script.
$PSNativeCommandUseErrorActionPreference = $false

if ([string]::IsNullOrWhiteSpace($env:NUGET_API_KEY)) {
    throw 'NUGET_API_KEY is not set. The publish step must expose the token from the NuGet login step.'
}

if (-not (Test-Path -LiteralPath $PackagePath -PathType Leaf)) {
    throw "NuGet package not found: $PackagePath"
}

$id = $PackageId.ToLowerInvariant()
$normalizedVersion = $Version.ToLowerInvariant()
$feedUrl = "$FlatContainerBaseUrl/$id/$normalizedVersion/$id.$normalizedVersion.nupkg"

# The flat container lags a push by seconds to minutes, so the retries stop a duplicate
# rejection from being reported as a package that never reached the feed.
function Test-Published([int[]]$RetryDelaysSeconds = @()) {
    foreach ($delay in @(0) + $RetryDelaysSeconds) {
        if ($delay -gt 0) { Start-Sleep -Seconds $delay }
        if ((Invoke-WebRequest -Uri $feedUrl -Method Head -SkipHttpErrorCheck).StatusCode -eq 200) { return $true }
    }
    return $false
}

$recovery = "NuGet.org will not accept this version again. If a previous run published it but failed before attaching evidence, attach that run's retained nupkg-signed, sbom, release-evidence and dependency-scan artifacts to the release manually."

if (Test-Published) {
    throw "$PackageId $Version is already on NuGet.org, so this run must not attach its evidence to the release: the published package may be a different build. $recovery"
}

dotnet nuget push $PackagePath --api-key $env:NUGET_API_KEY --source $Source
if ($LASTEXITCODE -eq 0) {
    Write-Host "Published $PackageId $Version."
    exit 0
}

Write-Host '::warning title=Push reported a failure::Checking whether the package reached NuGet.org anyway.'
if (Test-Published -RetryDelaysSeconds @(5, 15, 30)) {
    throw "dotnet nuget push reported a failure but $PackageId $Version is on NuGet.org. $recovery"
}

throw "dotnet nuget push failed and $PackageId $Version is not on NuGet.org."
