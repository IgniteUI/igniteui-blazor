<#
.SYNOPSIS
    Verifies that every assembly under the given path is Authenticode signed by an approved certificate.

.DESCRIPTION
    A valid Authenticode signature only proves that *someone* signed the file. This script additionally
    requires the signer certificate's SHA-256 fingerprint to appear in a list pinned in the repository,
    so a signature produced with any other certificate is rejected rather than trusted.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string[]]$Path,

    [Parameter(Mandatory)]
    [string]$ExpectedCertificateSha256Path,

    [string]$SummaryPath = $env:GITHUB_STEP_SUMMARY
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $ExpectedCertificateSha256Path -PathType Leaf)) {
    throw "Pinned certificate fingerprint file not found: $ExpectedCertificateSha256Path"
}

$allowedFingerprints = @(
    Get-Content -LiteralPath $ExpectedCertificateSha256Path |
        ForEach-Object { $_.Trim().ToUpperInvariant() } |
        Where-Object { $_ -and -not $_.StartsWith('#') }
)

# A blank or malformed pin must fail loudly; otherwise the whole check silently becomes a no-op.
if ($allowedFingerprints.Count -eq 0 -or @($allowedFingerprints | Where-Object { $_ -notmatch '^[0-9A-F]{64}$' }).Count -gt 0) {
    throw "$ExpectedCertificateSha256Path must contain at least one valid SHA-256 certificate fingerprint."
}

$assemblies = @(Get-ChildItem -Path $Path -Filter '*.dll' -Recurse -File)
if ($assemblies.Count -eq 0) {
    throw "No DLLs were found under '$($Path -join ', ')'. Refusing to report success."
}

$problems = @()
$fingerprints = @{}
$signerNames = @{}
foreach ($assembly in $assemblies) {
    $signature = Get-AuthenticodeSignature -LiteralPath $assembly.FullName
    if ($signature.Status -ne 'Valid') {
        $problems += "$($assembly.FullName): signature status $($signature.Status)."
        continue
    }

    $fingerprint = [Convert]::ToHexString(
        [System.Security.Cryptography.SHA256]::HashData($signature.SignerCertificate.RawData)
    )
    if ($fingerprint -notin $allowedFingerprints) {
        $problems += "$($assembly.FullName): certificate SHA-256 fingerprint $fingerprint is not approved by '$ExpectedCertificateSha256Path'."
        continue
    }

    $fingerprints[$fingerprint] = $true
    $signerNames[$signature.SignerCertificate.GetNameInfo('SimpleName', $false)] = $true
}

if ($problems.Count -gt 0) {
    throw "Authenticode validation failed:`n$($problems -join "`n")"
}

Write-Host "All $($assemblies.Count) DLLs signed by an approved certificate."

if ($SummaryPath) {
    @(
        '### Authenticode signer',
        "- Subject CN: $($signerNames.Keys -join ', ')",
        "- SHA-256 fingerprint: $($fingerprints.Keys -join ', ')"
    ) | Add-Content -LiteralPath $SummaryPath
}
