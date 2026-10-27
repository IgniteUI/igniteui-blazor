<#
.SYNOPSIS
    Verifies that a NuGet package is signed by an approved certificate.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    [Parameter(Mandatory)]
    [string]$ExpectedCertificateSha256Path
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $false

if (-not (Test-Path -LiteralPath $PackagePath -PathType Leaf)) {
    throw "NuGet package not found: $PackagePath"
}

if (-not (Test-Path -LiteralPath $ExpectedCertificateSha256Path -PathType Leaf)) {
    throw "Pinned certificate fingerprint file not found: $ExpectedCertificateSha256Path"
}

$allowedFingerprints = @(
    Get-Content -LiteralPath $ExpectedCertificateSha256Path |
        ForEach-Object { $_.Trim().ToUpperInvariant() } |
        Where-Object { $_ -and -not $_.StartsWith('#') }
)

if ($allowedFingerprints.Count -eq 0 -or @($allowedFingerprints | Where-Object { $_ -notmatch '^[0-9A-F]{64}$' }).Count -gt 0) {
    throw "$ExpectedCertificateSha256Path must contain at least one valid SHA-256 certificate fingerprint."
}

$verifyArguments = @('nuget', 'verify', $PackagePath, '--all')
foreach ($fingerprint in $allowedFingerprints) {
    $verifyArguments += @('--certificate-fingerprint', $fingerprint)
}
$verifyArguments += @('--verbosity', 'quiet')

& dotnet @verifyArguments
if ($LASTEXITCODE -ne 0) {
    throw "NuGet signature validation failed or the signer is not approved by '$ExpectedCertificateSha256Path'."
}

Write-Host "NuGet package signature matches an approved certificate."
