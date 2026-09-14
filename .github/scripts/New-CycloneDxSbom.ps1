<#
.SYNOPSIS
    Generates a CycloneDX SBOM for the project's .NET dependencies via dotnet-CycloneDX.

.DESCRIPTION
    This is the .NET half of the CycloneDX picture only: dotnet-CycloneDX has no notion of npm packages,
    so it cannot see the webpack bundle or the igniteui-webcomponents theme CSS the .nupkg also ships.
    New-NpmCycloneDxSbom.ps1 covers that half, and Merge-CycloneDxSbom.ps1 combines the two into the
    single document that is actually checksummed and attested; this script's output is an intermediate.

    dotnet-CycloneDX reads licence expressions and authors from each package's nuspec; when GitHub licence
    resolution is enabled below, unresolved file-based licences can additionally require authenticated
    GitHub API requests. This is what fills in fields the ClearlyDefined-backed SPDX documents otherwise
    leave as NOASSERTION whenever that service is degraded.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$ProjectPath,

    [Parameter(Mandatory)]
    [string]$PackageId,

    [Parameter(Mandatory)]
    [string]$PackageVersion,

    [Parameter(Mandatory)]
    [string]$OutputDirectory,

    # Resolves licences for packages whose nuspec points at a licence file instead of an SPDX expression.
    # In Actions, supply github.token; without it those packages are left unlicensed.
    [string]$GitHubBearerToken,

    # Pinned to match cyclonedx-npm's max supported version (1.6, vs this tool's own default of 1.7), so
    # Merge-CycloneDxSbom.ps1 combines two documents of the same spec version rather than mismatched ones.
    [string]$SpecVersion = '1.6'
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null

$arguments = @(
    $ProjectPath
    '--output', $OutputDirectory
    '--json'
    '--set-name', $PackageId
    '--set-version', $PackageVersion
    '--set-type', 'Library'
    '--spec-version', $SpecVersion
    '--exclude-dev'
    '--include-license-text'
)

if ($GitHubBearerToken) {
    $env:CYCLONEDX_GITHUB_BEARER_TOKEN = $GitHubBearerToken
    $arguments += '--enable-github-licenses'
}
else {
    Write-Warning 'No GitHub token supplied; packages that declare a licence file rather than an SPDX expression will be left unlicensed.'
}

try {
    dotnet tool run dotnet-CycloneDX -- @arguments
}
finally {
    Remove-Item Env:\CYCLONEDX_GITHUB_BEARER_TOKEN -ErrorAction SilentlyContinue
}

$bomPath = Join-Path $OutputDirectory 'bom.json'
if (-not (Test-Path -LiteralPath $bomPath -PathType Leaf)) {
    throw "CycloneDX did not produce a document at $bomPath."
}

$bom = Get-Content -LiteralPath $bomPath -Raw | ConvertFrom-Json
$components = @($bom.components)

if ($components.Count -eq 0) {
    throw '.NET CycloneDX document contains no components.'
}

Write-Host ".NET CycloneDX $($bom.specVersion): $($components.Count) components, $(@($bom.dependencies).Count) dependency edges."
