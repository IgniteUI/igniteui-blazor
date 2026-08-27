# Measures the shipped static web assets against eng/bundle-budgets.json and writes the release's
# performance evidence. Bundle filenames are content-hashed, so budgets are patterns and every file
# must match exactly one group -- an asset nobody budgeted for fails rather than passing silently.
[CmdletBinding()]
param(
    [string]$BudgetPath = "$PSScriptRoot/bundle-budgets.json",

    [string]$Root,

    [string]$OutputDirectory,

    # Measure without failing. Use when reseeding budgets after an intentional increase.
    [switch]$ReportOnly
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $BudgetPath -PathType Leaf)) {
    throw "Budget file not found: $BudgetPath"
}

$repositoryRoot = (Resolve-Path -LiteralPath "$PSScriptRoot/..").ProviderPath
$budget = Get-Content -LiteralPath $BudgetPath -Raw | ConvertFrom-Json

if (-not $Root) { $Root = Join-Path $repositoryRoot $budget.root }
if (-not $OutputDirectory) { $OutputDirectory = Join-Path $repositoryRoot 'artifacts/perf' }

if (-not (Test-Path -LiteralPath $Root -PathType Container)) {
    throw "Asset root '$Root' does not exist. Run 'npm run build' and 'npm run copythemes' first."
}

function Measure-GzipLength([string]$FilePath) {
    $bytes = [System.IO.File]::ReadAllBytes($FilePath)
    $buffer = New-Object System.IO.MemoryStream
    try {
        $gzip = New-Object System.IO.Compression.GZipStream($buffer, [System.IO.Compression.CompressionLevel]::Optimal, $true)
        try {
            $gzip.Write($bytes, 0, $bytes.Length)
        }
        finally {
            $gzip.Dispose()
        }

        return $buffer.Length
    }
    finally {
        $buffer.Dispose()
    }
}

$rootPath = (Resolve-Path -LiteralPath $Root).ProviderPath
$files = @(Get-ChildItem -LiteralPath $rootPath -Recurse -File)
if ($files.Count -eq 0) {
    throw "No files found under '$rootPath'. Refusing to report a passing budget for an empty build."
}

$measurements = @()
$unmatched = @()
foreach ($file in $files) {
    $relativePath = $file.FullName.Substring($rootPath.Length).TrimStart('\', '/').Replace('\', '/')

    # First match wins, so eng/bundle-budgets.json orders specific patterns before catch-alls.
    $group = $budget.groups | Where-Object {
        $pattern = $_.include | Where-Object { $relativePath -like $_ }
        $null -ne $pattern
    } | Select-Object -First 1

    if ($null -eq $group) {
        $unmatched += $relativePath
        continue
    }

    $measurements += [pscustomobject]@{
        Path     = $relativePath
        Group    = $group.id
        RawBytes = $file.Length
        GzipBytes = Measure-GzipLength $file.FullName
    }
}

function ConvertTo-KiB([long]$Bytes) {
    return [math]::Round($Bytes / 1KB, 1)
}

$problems = @()
if ($unmatched.Count -gt 0) {
    $problems += "These assets match no budget group in $([System.IO.Path]::GetFileName($BudgetPath)); add a group for them: $($unmatched -join ', ')"
}

$groupResults = @()
foreach ($group in $budget.groups) {
    $groupFiles = @($measurements | Where-Object { $_.Group -eq $group.id })
    $raw = ($groupFiles | Measure-Object -Property RawBytes -Sum).Sum
    $gzip = ($groupFiles | Measure-Object -Property GzipBytes -Sum).Sum
    if ($null -eq $raw) { $raw = 0 }
    if ($null -eq $gzip) { $gzip = 0 }

    $result = [pscustomobject]@{
        Id          = $group.id
        Description = $group.description
        FileCount   = $groupFiles.Count
        RawKiB      = ConvertTo-KiB $raw
        MaxRawKiB   = $group.maxRawKiB
        GzipKiB     = ConvertTo-KiB $gzip
        MaxGzipKiB  = $group.maxGzipKiB
        Files       = @($groupFiles | ForEach-Object { $_.Path })
    }
    $groupResults += $result

    if ($result.RawKiB -gt $group.maxRawKiB) {
        $problems += "Group '$($group.id)' is $($result.RawKiB) KiB raw, over its $($group.maxRawKiB) KiB budget."
    }
    if ($null -ne $group.maxGzipKiB -and $result.GzipKiB -gt $group.maxGzipKiB) {
        $problems += "Group '$($group.id)' is $($result.GzipKiB) KiB gzipped, over its $($group.maxGzipKiB) KiB budget."
    }
}

$totalResults = @()
foreach ($total in $budget.totals) {
    $included = if ($total.groups -contains '*') { $measurements } else { $measurements | Where-Object { $total.groups -contains $_.Group } }
    $included = @($included)
    $raw = ($included | Measure-Object -Property RawBytes -Sum).Sum
    $gzip = ($included | Measure-Object -Property GzipBytes -Sum).Sum
    if ($null -eq $raw) { $raw = 0 }
    if ($null -eq $gzip) { $gzip = 0 }

    $result = [pscustomobject]@{
        Id          = $total.id
        Description = $total.description
        FileCount   = $included.Count
        RawKiB      = ConvertTo-KiB $raw
        MaxRawKiB   = $total.maxRawKiB
        GzipKiB     = ConvertTo-KiB $gzip
        MaxGzipKiB  = $total.maxGzipKiB
    }
    $totalResults += $result

    if ($result.RawKiB -gt $total.maxRawKiB) {
        $problems += "Total '$($total.id)' is $($result.RawKiB) KiB raw, over its $($total.maxRawKiB) KiB budget."
    }
    if ($null -ne $total.maxGzipKiB -and $result.GzipKiB -gt $total.maxGzipKiB) {
        $problems += "Total '$($total.id)' is $($result.GzipKiB) KiB gzipped, over its $($total.maxGzipKiB) KiB budget."
    }
}

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
$reportJsonPath = Join-Path $OutputDirectory 'performance-report.json'
$reportMarkdownPath = Join-Path $OutputDirectory 'performance-report.md'

[pscustomobject]@{
    measuredAtUtc = (Get-Date).ToUniversalTime().ToString('o')
    assetRoot     = $budget.root
    budgetFile    = (Split-Path -Leaf $BudgetPath)
    passed        = ($problems.Count -eq 0)
    problems      = $problems
    groups        = $groupResults
    totals        = $totalResults
    files         = @($measurements | Sort-Object -Property RawBytes -Descending | ForEach-Object {
            [pscustomobject]@{
                path    = $_.Path
                group   = $_.Group
                rawKiB  = ConvertTo-KiB $_.RawBytes
                gzipKiB = ConvertTo-KiB $_.GzipBytes
            }
        })
} | ConvertTo-Json -Depth 6 | Set-Content -LiteralPath $reportJsonPath -Encoding utf8

$markdown = New-Object System.Collections.Generic.List[string]
$markdown.Add('## Bundle size budget')
$markdown.Add('')
$markdown.Add("Measured `$($budget.root)` on $((Get-Date).ToUniversalTime().ToString('yyyy-MM-dd HH:mm')) UTC.")
$markdown.Add('')
$markdown.Add('| Total | Raw KiB | Budget | Gzip KiB | Budget |')
$markdown.Add('| --- | ---: | ---: | ---: | ---: |')
foreach ($total in $totalResults) {
    $gzipBudget = if ($null -ne $total.MaxGzipKiB) { $total.MaxGzipKiB } else { 'n/a' }
    $gzipValue = if ($null -ne $total.MaxGzipKiB) { $total.GzipKiB } else { 'n/a' }
    $markdown.Add("| $($total.Id) | $($total.RawKiB) | $($total.MaxRawKiB) | $gzipValue | $gzipBudget |")
}
$markdown.Add('')
$markdown.Add('| Group | Files | Raw KiB | Budget | Gzip KiB | Budget |')
$markdown.Add('| --- | ---: | ---: | ---: | ---: | ---: |')
foreach ($group in $groupResults) {
    $gzipBudget = if ($null -ne $group.MaxGzipKiB) { $group.MaxGzipKiB } else { 'n/a' }
    $gzipValue = if ($null -ne $group.MaxGzipKiB) { $group.GzipKiB } else { 'n/a' }
    $markdown.Add("| $($group.Id) | $($group.FileCount) | $($group.RawKiB) | $($group.MaxRawKiB) | $gzipValue | $gzipBudget |")
}
$markdown.Add('')
if ($problems.Count -gt 0) {
    $markdown.Add('### Budget breaches')
    $markdown.Add('')
    foreach ($problem in $problems) { $markdown.Add("- $problem") }
}
else {
    $markdown.Add('All assets are within budget.')
}

$markdown | Set-Content -LiteralPath $reportMarkdownPath -Encoding utf8

if ($env:GITHUB_STEP_SUMMARY) {
    $markdown | Add-Content -LiteralPath $env:GITHUB_STEP_SUMMARY -Encoding utf8
}

Write-Host "Wrote $reportJsonPath"
Write-Host "Wrote $reportMarkdownPath"
$totalResults | Format-Table -Property Id, RawKiB, MaxRawKiB, GzipKiB, MaxGzipKiB -AutoSize | Out-String | Write-Host

if ($problems.Count -gt 0) {
    $message = "Bundle size budget failed:`n- $($problems -join "`n- ")"
    if ($ReportOnly) {
        Write-Warning $message
    }
    else {
        throw $message
    }
}
else {
    Write-Host 'All assets are within budget.'
}
