<#
.SYNOPSIS
    Verifies the merged CycloneDX document describes both the .NET and npm halves of the shipped package.

.DESCRIPTION
    Checks the structural fields actions/attest requires, and specifically that at least one nuget- and
    one npm-ecosystem component are present. That is the concrete regression this guards against: a merge
    failure that still produces a valid-looking, structurally sound, but incomplete document. License and
    author coverage are reported as warnings only, same as the SPDX side - NOASSERTION/missing is a valid
    value, not something this tool can require otherwise.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$BomPath,

    [ValidateRange(0, 1)]
    [double]$MinimumLicenseCoverage = 0.9
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $BomPath -PathType Leaf)) {
    throw "CycloneDX document not found: $BomPath"
}

$bom = Get-Content -LiteralPath $BomPath -Raw | ConvertFrom-Json

# actions/attest only recognises a CycloneDX document that carries all three of these.
foreach ($required in 'bomFormat', 'specVersion', 'serialNumber') {
    if (-not $bom.$required) {
        throw "CycloneDX document is missing '$required', so it cannot be consumed as an SBOM predicate."
    }
}

$components = @($bom.components)
if ($components.Count -eq 0) {
    throw 'CycloneDX document contains no components.'
}

$nugetComponents = @($components | Where-Object { $_.purl -like 'pkg:nuget/*' })
$npmComponents = @($components | Where-Object { $_.purl -like 'pkg:npm/*' })

if ($nugetComponents.Count -eq 0) {
    throw 'Merged CycloneDX document contains no pkg:nuget components; the .NET BOM appears to be missing from the merge.'
}
if ($npmComponents.Count -eq 0) {
    throw 'Merged CycloneDX document contains no pkg:npm components; the npm BOM appears to be missing from the merge.'
}

$licensed = @($components | Where-Object { $_.licenses })
# CycloneDX models 'authors' (people) and 'supplier' (an organisation) separately; cyclonedx-dotnet only
# ever populates the former, so this is reported as author coverage.
$authored = @($components | Where-Object { $_.authors })
$coverage = $licensed.Count / $components.Count

Write-Host "CycloneDX $($bom.specVersion): $($components.Count) components ($($nugetComponents.Count) NuGet, $($npmComponents.Count) npm), $($licensed.Count) licensed, $($authored.Count) with an author."

if ($coverage -lt $MinimumLicenseCoverage) {
    $unlicensed = @($components | Where-Object { -not $_.licenses } | ForEach-Object { "$($_.name)@$($_.version)" })
    Write-Warning "Only $([math]::Round($coverage * 100))% of components carry a licence (threshold $([math]::Round($MinimumLicenseCoverage * 100))%). Unresolved: $($unlicensed -join ', ')"
}

$checksum = (Get-FileHash -LiteralPath $BomPath -Algorithm SHA256).Hash.ToLowerInvariant()
Set-Content -LiteralPath "$BomPath.sha256" -Value $checksum -NoNewline
Write-Host "Wrote checksum sidecar $BomPath.sha256."
