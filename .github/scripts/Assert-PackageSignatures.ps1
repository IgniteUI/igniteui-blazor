<#
.SYNOPSIS
    Verifies the strong-name and Authenticode signatures of the assemblies inside a packed NuGet package.

.DESCRIPTION
    Packing does not sign or rebuild; -no-build just re-zips whatever bin/ output is on disk. The prior
    jobs validate that loose output directly, but that is not proof the packed nupkg contains those exact
    bytes. This extracts the package that will actually ship and re-runs both checks against those bytes
    in a single extraction, so a pack step that picked up a stale or substituted DLL is caught either way,
    not just on the weaker (SHA-1 strong-name) signal.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    [Parameter(Mandatory)]
    [string]$ExpectedPublicKeyPath,

    [Parameter(Mandatory)]
    [string]$ExpectedCertificateSha256Path,

    [string]$WorkingDirectory = (Join-Path ([System.IO.Path]::GetTempPath()) 'package-signature-validation')
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $PackagePath -PathType Leaf)) {
    throw "NuGet package not found: $PackagePath"
}

$extractPath = Join-Path $WorkingDirectory 'package'
# Expand-Archive only accepts .zip, so the package is copied under a name it will open.
$archivePath = Join-Path $WorkingDirectory 'package.zip'

try {
    New-Item -ItemType Directory -Path $WorkingDirectory -Force | Out-Null
    Copy-Item -LiteralPath $PackagePath -Destination $archivePath -Force
    Expand-Archive -LiteralPath $archivePath -DestinationPath $extractPath -Force

    & (Join-Path $PSScriptRoot 'Assert-AssemblyStrongName.ps1') -Path $extractPath -ExpectedPublicKeyPath $ExpectedPublicKeyPath
    & (Join-Path $PSScriptRoot 'Assert-AuthenticodeSignature.ps1') -Path $extractPath -ExpectedCertificateSha256Path $ExpectedCertificateSha256Path
}
finally {
    Remove-Item -LiteralPath $WorkingDirectory -Recurse -Force -ErrorAction SilentlyContinue
}
