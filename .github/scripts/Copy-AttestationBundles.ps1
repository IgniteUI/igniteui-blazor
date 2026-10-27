<#
.SYNOPSIS
    Copies the provenance and SBOM attestation bundles next to the SBOM documents and records their URLs.

.DESCRIPTION
    actions/attest writes each bundle to a temporary path that only the producing job can read, so the
    bundles are collected into the release artifact while they are still reachable.

    The two SBOM attestations carry different predicate types - https://spdx.dev/Document/v2.2 and
    https://cyclonedx.org/bom - so a verifier can ask for either without ambiguity.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$ProvenanceBundlePath,

    [Parameter(Mandatory)]
    [string]$SpdxBundlePath,

    [Parameter(Mandatory)]
    [string]$CycloneDxBundlePath,

    [Parameter(Mandatory)]
    [string]$Destination,

    [string]$ProvenanceUrl,

    [string]$SpdxUrl,

    [string]$CycloneDxUrl,

    [string]$SummaryPath = $env:GITHUB_STEP_SUMMARY
)

$ErrorActionPreference = 'Stop'

New-Item -ItemType Directory -Path $Destination -Force | Out-Null
Copy-Item -LiteralPath $ProvenanceBundlePath -Destination (Join-Path $Destination 'provenance.sigstore.json') -Force
Copy-Item -LiteralPath $SpdxBundlePath -Destination (Join-Path $Destination 'sbom-spdx.sigstore.json') -Force
Copy-Item -LiteralPath $CycloneDxBundlePath -Destination (Join-Path $Destination 'sbom-cyclonedx.sigstore.json') -Force

if ($SummaryPath) {
    @(
        '### Attestations',
        "- Provenance: $ProvenanceUrl",
        "- SBOM (SPDX 2.2): $SpdxUrl",
        "- SBOM (CycloneDX): $CycloneDxUrl"
    ) | Add-Content -LiteralPath $SummaryPath
}
