<#
.SYNOPSIS
    Verifies the generated SPDX 2.2 and 3.0 manifests and their relationship to the shipped package.

.DESCRIPTION
    Requires parseable, structurally valid documents, verifies each manifest against its SHA-256 sidecar,
    and proves that the SPDX 2.2 file entry describes the exact package bytes being released.

    Also guards the two failure modes sbom-tool does not report: a manifest that ended up inside the
    component scan root and so describes itself, and a ClearlyDefined outage that silently replaces every
    license with NOASSERTION. A partial gap is reported rather than fatal - NOASSERTION is a valid SPDX
    value and the upstream tool offers no way to require otherwise. Every package landing on NOASSERTION
    is fatal instead: observed outages have been all-or-nothing (0 of N resolved), not a gradual
    plateau, so a zero-license document is the outage signature rather than a coverage shortfall.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$OutputRoot,

    [Parameter(Mandatory)]
    [string]$ExpectedPackagePath,

    # Below this share of packages carrying a resolved license, the run is annotated rather than failed.
    # Zero resolved licenses fails regardless of this threshold - see the check below.
    [ValidateRange(0, 1)]
    [double]$MinimumLicenseCoverage = 0.8
)

$ErrorActionPreference = 'Stop'

$manifests = @(
    [pscustomobject]@{
        Name = 'SPDX 2.2'
        Path = Join-Path $OutputRoot '_manifest/spdx_2.2/manifest.spdx.json'
    },
    [pscustomobject]@{
        Name = 'SPDX 3.0'
        Path = Join-Path $OutputRoot '_manifest/spdx_3.0/manifest.spdx.json'
    }
)

$problems = @()
$documents = @{}
foreach ($manifest in $manifests) {
    if (-not (Test-Path -LiteralPath $manifest.Path -PathType Leaf)) {
        $problems += "$($manifest.Name) document missing: $($manifest.Path)"
        continue
    }

    if ((Get-Item -LiteralPath $manifest.Path).Length -eq 0) {
        $problems += "$($manifest.Name) document is empty: $($manifest.Path)"
        continue
    }

    try {
        $documents[$manifest.Name] = Get-Content -LiteralPath $manifest.Path -Raw | ConvertFrom-Json -ErrorAction Stop
    }
    catch {
        $problems += "$($manifest.Name) document is not valid JSON: $($_.Exception.Message)"
    }

    $checksumPath = "$($manifest.Path).sha256"
    if (-not (Test-Path -LiteralPath $checksumPath -PathType Leaf)) {
        $problems += "$($manifest.Name) checksum missing: $checksumPath"
        continue
    }

    $recordedChecksum = (Get-Content -LiteralPath $checksumPath -Raw).Trim().ToLowerInvariant()
    if ($recordedChecksum -notmatch '^[0-9a-f]{64}$') {
        $problems += "$($manifest.Name) checksum sidecar does not contain one SHA-256 digest: $checksumPath"
        continue
    }

    $actualChecksum = (Get-FileHash -LiteralPath $manifest.Path -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($recordedChecksum -ne $actualChecksum) {
        $problems += "$($manifest.Name) checksum is $recordedChecksum, but the document hash is $actualChecksum."
    }
}

if ($documents.ContainsKey('SPDX 2.2')) {
    $spdx22 = $documents['SPDX 2.2']
    if ($spdx22.spdxVersion -ne 'SPDX-2.2') {
        $problems += "SPDX 2.2 document declares version '$($spdx22.spdxVersion)'."
    }
    if (@($spdx22.packages).Count -eq 0) {
        $problems += 'SPDX 2.2 document contains no packages.'
    }
    if (@($spdx22.files).Count -eq 0) {
        $problems += 'SPDX 2.2 document contains no files.'
    }
}

if ($documents.ContainsKey('SPDX 3.0')) {
    $spdx30 = $documents['SPDX 3.0']
    if (@($spdx30.'@context').Count -eq 0) {
        $problems += 'SPDX 3.0 document contains no @context.'
    }
    if (@($spdx30.'@graph').Count -eq 0) {
        $problems += 'SPDX 3.0 document contains no @graph entries.'
    }
}

if ($documents.ContainsKey('SPDX 2.2') -and $documents.ContainsKey('SPDX 3.0')) {
    $graph = @($documents['SPDX 3.0'].'@graph')

    # Either document describing an SBOM manifest means the output landed inside the scanned tree.
    $selfReferences = @(
        @($documents['SPDX 2.2'].files | Where-Object { $_.fileName -like '*manifest.spdx.json' }) +
        @($graph | Where-Object { $_.type -eq 'software_File' -and $_.name -like '*manifest.spdx.json' })
    )
    if ($selfReferences.Count -gt 0) {
        $problems += "The SBOMs describe $($selfReferences.Count) SBOM manifest file(s) as build content. Generate them outside the component scan root."
    }

    $packages22 = @($documents['SPDX 2.2'].packages).Count
    $packages30 = @($graph | Where-Object { $_.type -eq 'software_Package' }).Count
    if ($packages22 -ne $packages30) {
        $problems += "SPDX 2.2 records $packages22 packages but SPDX 3.0 records $packages30. The two formats must describe the same build."
    }
}

if (-not (Test-Path -LiteralPath $ExpectedPackagePath -PathType Leaf)) {
    $problems += "Expected NuGet package missing: $ExpectedPackagePath"
}
elseif ($documents.ContainsKey('SPDX 2.2')) {
    $expectedFileName = [System.IO.Path]::GetFileName($ExpectedPackagePath)
    $expectedPackageHash = (Get-FileHash -LiteralPath $ExpectedPackagePath -Algorithm SHA256).Hash.ToLowerInvariant()
    $packageFiles = @(
        $documents['SPDX 2.2'].files | Where-Object {
            $_.fileName -and [System.IO.Path]::GetFileName([string]$_.fileName) -eq $expectedFileName
        }
    )

    if ($packageFiles.Count -ne 1) {
        $problems += "SPDX 2.2 document contains $($packageFiles.Count) file entries for $expectedFileName; expected exactly one."
    }
    else {
        $recordedPackageHashes = @(
            $packageFiles[0].checksums |
                Where-Object { $_.algorithm -eq 'SHA256' } |
                ForEach-Object { ([string]$_.checksumValue).ToLowerInvariant() }
        )

        if ($expectedPackageHash -notin $recordedPackageHashes) {
            $problems += "SPDX 2.2 records SHA-256 '$($recordedPackageHashes -join ', ')' for $expectedFileName, but the package hash is $expectedPackageHash."
        }
    }
}

if ($problems.Count -gt 0) {
    throw "SBOM validation failed:`n- $($problems -join "`n- ")"
}

$packages = @($documents['SPDX 2.2'].packages)
$licensed = @($packages | Where-Object { $_.licenseConcluded -and $_.licenseConcluded -ne 'NOASSERTION' })
$coverage = if ($packages.Count -gt 0) { $licensed.Count / $packages.Count } else { 0 }

Write-Host "SBOM covers $($packages.Count) packages and $(@($documents['SPDX 2.2'].files).Count) files, including the verified NuGet package."
Write-Host "License coverage: $($licensed.Count) of $($packages.Count) packages ($([math]::Round($coverage * 100))%)."

# Zero is the outage signature (ClearlyDefined unreachable for the whole run), not a coverage shortfall -
# New-Sbom.ps1's retry loop already tried to recover from this and gave up, so fail rather than ship it.
if ($packages.Count -gt 0 -and $licensed.Count -eq 0) {
    throw "No package in the SPDX document carries a resolved license (0 of $($packages.Count)). This is the ClearlyDefined-outage signature, not partial degradation - refusing to ship an SBOM with no license data."
}

if ($coverage -lt $MinimumLicenseCoverage) {
    $unlicensed = @($packages | Where-Object { -not $_.licenseConcluded -or $_.licenseConcluded -eq 'NOASSERTION' } | ForEach-Object { "$($_.name)@$($_.versionInfo)" })
    Write-Warning "Only $([math]::Round($coverage * 100))% of packages carry a resolved license (threshold $([math]::Round($MinimumLicenseCoverage * 100))%). Unresolved: $($unlicensed -join ', ')"
}

$reciprocal = @($packages | Where-Object { $_.licenseConcluded -match 'GPL|RPL|MPL|EPL|CDDL|OSL|SSPL' })
if ($reciprocal.Count -gt 0) {
    Write-Warning "Reciprocal or copyleft licenses detected: $(($reciprocal | ForEach-Object { "$($_.name)@$($_.versionInfo) ($($_.licenseConcluded))" }) -join '; ')"
}
