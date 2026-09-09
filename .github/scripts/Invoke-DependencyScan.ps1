<#
.SYNOPSIS
    Records the known vulnerabilities in the NuGet and npm dependencies the package ships.

.DESCRIPTION
    Advisory by design. A finding is annotated and attached to the release as evidence but never holds
    up the publish; the blocking gate for newly introduced vulnerable dependencies is the PR-time
    dependency-review check. A scan that fails to run, however, is an error: silence must not be
    mistaken for a clean result.

    Both ecosystems are scanned because the .nupkg ships both - the .NET assemblies and the bundled
    JavaScript built from the npm runtime dependencies. npm is scanned with --omit=dev for the same
    reason: build tooling is not part of the shipped artifact.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$ProjectPath,

    [Parameter(Mandatory)]
    [string]$OutputDirectory,

    # Directory holding the package.json/package-lock.json pair. Omit to skip the npm half.
    [string]$NpmManifestDirectory,

    [string]$PackageId,

    [string]$Version,

    [string]$SummaryPath = $env:GITHUB_STEP_SUMMARY
)

$ErrorActionPreference = 'Stop'
# Each scan's exit code is inspected explicitly so a failure can be reported with context.
$PSNativeCommandUseErrorActionPreference = $false

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null

$nugetReportPath = Join-Path $OutputDirectory 'nuget-vulnerable.json'

dotnet restore $ProjectPath | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw "dotnet restore failed for $ProjectPath with exit code $LASTEXITCODE."
}

dotnet list $ProjectPath package --vulnerable --include-transitive --format json --output-version 1 |
    Set-Content -LiteralPath $nugetReportPath -Encoding utf8
if ($LASTEXITCODE -ne 0) {
    throw "dotnet list package --vulnerable failed with exit code $LASTEXITCODE."
}

$nugetReport = Get-Content -LiteralPath $nugetReportPath -Raw | ConvertFrom-Json
if ($nugetReport.version -ne 1 -or -not $nugetReport.projects) {
    throw 'dotnet list package did not produce a valid version 1 JSON report.'
}

$nugetFindings = @(
    foreach ($project in $nugetReport.projects) {
        # A framework with no findings omits the package arrays entirely, so null entries are dropped.
        foreach ($framework in @($project.frameworks | Where-Object { $_ })) {
            $packages = @($framework.topLevelPackages) + @($framework.transitivePackages)
            foreach ($package in @($packages | Where-Object { $_ })) {
                foreach ($vulnerability in @($package.vulnerabilities | Where-Object { $_ })) {
                    [pscustomobject]@{
                        Framework   = $framework.framework
                        Package     = $package.id
                        Resolved    = $package.resolvedVersion
                        Severity    = $vulnerability.severity
                        AdvisoryUrl = $vulnerability.advisoryurl
                    }
                }
            }
        }
    }
)

$nugetTable = if ($nugetFindings.Count -gt 0) {
    $nugetFindings | Sort-Object Severity, Package, Framework | Format-Table -AutoSize | Out-String -Width 200
}
else {
    'No vulnerable shipped NuGet dependencies reported.'
}

$nugetTable | Set-Content -LiteralPath (Join-Path $OutputDirectory 'nuget-vulnerable.txt') -Encoding utf8
Write-Host $nugetTable

$npmFindings = @()
$npmTable = $null
if ($NpmManifestDirectory) {
    if (-not (Test-Path -LiteralPath (Join-Path $NpmManifestDirectory 'package-lock.json') -PathType Leaf)) {
        throw "npm audit needs a lockfile, but none was found in $NpmManifestDirectory."
    }

    $npmReportPath = Join-Path $OutputDirectory 'npm-audit.json'

    Push-Location -LiteralPath $NpmManifestDirectory
    try {
        npm audit --omit=dev --audit-level=info --json | Set-Content -LiteralPath $npmReportPath -Encoding utf8
        $npmExitCode = $LASTEXITCODE
    }
    finally {
        Pop-Location
    }

    $npmReport = Get-Content -LiteralPath $npmReportPath -Raw | ConvertFrom-Json
    if ($null -eq $npmReport.auditReportVersion -or $null -eq $npmReport.metadata.vulnerabilities.total -or $npmReport.PSObject.Properties.Name -contains 'error') {
        throw 'npm audit did not produce a valid JSON report.'
    }

    $npmTotal = [int]$npmReport.metadata.vulnerabilities.total
    # npm audit exits 1 only because it found something; any other combination means the scan itself broke.
    if (-not (($npmExitCode -eq 0 -and $npmTotal -eq 0) -or ($npmExitCode -eq 1 -and $npmTotal -gt 0))) {
        throw "npm audit exited with status $npmExitCode while reporting $npmTotal vulnerabilities."
    }

    $npmFindings = @(
        foreach ($property in @($npmReport.vulnerabilities.PSObject.Properties)) {
            $vulnerability = $property.Value
            $advisories = @(
                $vulnerability.via |
                    Where-Object { $_ -is [System.Management.Automation.PSCustomObject] -and $_.url } |
                    ForEach-Object { $_.url }
            )

            [pscustomobject]@{
                Package     = $vulnerability.name
                Severity    = $vulnerability.severity
                Range       = $vulnerability.range
                Direct      = [bool]$vulnerability.isDirect
                AdvisoryUrl = ($advisories -join ' ')
            }
        }
    )

    $npmTable = if ($npmFindings.Count -gt 0) {
        $npmFindings | Sort-Object Severity, Package | Format-Table -AutoSize | Out-String -Width 200
    }
    else {
        'No vulnerable shipped npm dependencies reported.'
    }

    $npmTable | Set-Content -LiteralPath (Join-Path $OutputDirectory 'npm-audit.txt') -Encoding utf8
    Write-Host $npmTable
}

if (-not $SummaryPath) {
    return
}

$summary = @(
    '### Dependency vulnerability scan'
    ''
    'Advisory only - findings are recorded but do not block this release.'
    ''
    '<details><summary>dotnet list package --vulnerable --include-transitive</summary>'
    ''
    '```'
    $nugetTable.TrimEnd()
    '```'
    ''
    '</details>'
    ''
)

if ($null -ne $npmTable) {
    $summary += @(
        '<details><summary>npm audit --omit=dev</summary>'
        ''
        '```'
        $npmTable.TrimEnd()
        '```'
        ''
        '</details>'
        ''
    )
}

$total = $nugetFindings.Count + $npmFindings.Count
if ($total -gt 0) {
    $label = "$PackageId $Version".Trim()
    Write-Host "::warning title=Vulnerable dependencies reported::$label was released with $total dependency advisories outstanding. See the run summary and the dependency-scan release asset."
    $summary += "> [!WARNING]`n> $total vulnerable dependencies were reported for this release ($($nugetFindings.Count) NuGet, $($npmFindings.Count) npm). Review the scan output above and open a servicing issue if a fix is required."
}
else {
    $summary += "> [!NOTE]`n> No vulnerable shipped dependencies reported."
}

$summary | Add-Content -LiteralPath $SummaryPath
