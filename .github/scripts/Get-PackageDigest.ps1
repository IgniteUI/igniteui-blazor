<#
.SYNOPSIS
    Computes a package's SHA-256 digest and, optionally, asserts it against digests recorded earlier.

.DESCRIPTION
    The digest is the identity every downstream job re-checks before acting on the package, so that a
    job cannot pack, attest or publish bytes other than the ones that were signed.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    # Every value must match the computed digest. Empty entries are ignored so a caller can pass
    # job outputs directly without branching on which of them are set.
    [string[]]$ExpectedSha256 = @(),

    [string]$FailureMessage = 'Package digest changed between jobs.',

    # Writes '<digest>  <file name>' next to the package, in the format sha256sum expects.
    [switch]$WriteChecksumFile,

    [string]$GitHubOutputName,

    [string]$SummaryTitle,

    [string]$GitHubOutputPath = $env:GITHUB_OUTPUT,

    [string]$SummaryPath = $env:GITHUB_STEP_SUMMARY
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $PackagePath -PathType Leaf)) {
    throw "NuGet package not found: $PackagePath"
}

$name = [System.IO.Path]::GetFileName($PackagePath)
$digest = (Get-FileHash -LiteralPath $PackagePath -Algorithm SHA256).Hash.ToLowerInvariant()

foreach ($expected in @($ExpectedSha256 | Where-Object { $_ })) {
    if ($digest -ne $expected.Trim().ToLowerInvariant()) {
        throw "$FailureMessage Expected $expected but found $digest for $name."
    }
}

if ($WriteChecksumFile) {
    "$digest  $name" | Set-Content -LiteralPath "$PackagePath.sha256" -Encoding ascii
}

if ($GitHubOutputName -and $GitHubOutputPath) {
    "$GitHubOutputName=$digest" | Add-Content -LiteralPath $GitHubOutputPath
}

if ($SummaryTitle -and $SummaryPath) {
    @("### $SummaryTitle", '```', "$digest  $name", '```') | Add-Content -LiteralPath $SummaryPath
}

Write-Host "Verified $name with digest $digest."
