<#
.SYNOPSIS
    Hunt for flickers in the Bulk API integration tests by running them under CPU pressure.

.DESCRIPTION
    Flickers here tend to come from timing: the tests drive a Blazor server component
    through a browser, so a check can outrun whatever it is waiting on. A developer box
    with cores to spare hides that, and a handful of green runs on one says very little.
    Confining the run to a single CPU makes the server, the browser and everything they
    queue compete, which is usually enough to surface an ordering assumption. The
    confinement is applied at process creation and inherited, so the test host and the
    browser Playwright starts are both covered - that breadth is the point, and it is why
    DOTNET_PROCESSOR_COUNT=1 is not a substitute: it resizes the thread pool without
    making anything else compete for the core.

    A run counts as failed on any test failure. The patterns in $signatures only decide
    which lines get echoed for context, so add to them when chasing a failure they do
    not cover.

    Read the result as a rate, not a verdict: this raises the odds of a race losing, it
    does not make one certain, so a clean pass is evidence and not proof.

    For illustration, the #249 flickers (property values and events read before the
    wrapper had flushed them to the client) came up in roughly a third of single-CPU runs
    and in none unpinned.

.PARAMETER Affinity
    CPU mask in hex, the same form as cmd's start /affinity. 1 = CPU0 (default),
    3 = CPU0 and CPU1, F = CPU0 through CPU3. Fewer cores means more pressure.

.EXAMPLE
    .\flicker-repro.ps1
    IgbCalendar, 10 runs, one core.

.EXAMPLE
    .\flicker-repro.ps1 -Component IgbTile -Runs 20

.EXAMPLE
    .\flicker-repro.ps1 -Component '' -Runs 3
    The whole suite, around 50s per run.

.NOTES
    Exits non-zero if any run failed, so it can gate a bisect.
#>
[CmdletBinding()]
param(
    [string]$Component = 'IgbCalendar',
    [int]$Runs = 10,
    [string]$Affinity = '1',
    [switch]$SkipBuild
)

$ErrorActionPreference = 'Stop'

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..' '..')
$project = Join-Path $repoRoot 'tests' 'IgniteUI.Blazor.Lite.IntegrationTests'
$runsettings = Join-Path $repoRoot '.runsettings'
$onWindows = $IsWindows -or $env:OS -eq 'Windows_NT'

if (-not $SkipBuild) {
    # Rebuild by default: the runs below pass --no-build for speed, so against a stale
    # binary you would be testing the branch you came from and calling it clean.
    Write-Host 'building...'
    $buildLog = [IO.Path]::GetTempFileName()
    dotnet build $project -v q --nologo *> $buildLog
    if ($LASTEXITCODE -ne 0) {
        Select-String -Path $buildLog -Pattern 'error CS', ' error ' | Select-Object -First 20 | ForEach-Object { $_.Line }
        throw "build failed, see $buildLog"
    }
    Remove-Item $buildLog -ErrorAction SilentlyContinue
}

$filter = if ($Component) { "--filter `"FullyQualifiedName~$Component`"" } else { '' }

# All the quoting lives in a generated batch file, so PowerShell only has to run it and
# read the output back. start applies the affinity at creation time, which Start-Process
# plus a ProcessorAffinity assignment cannot promise - the test host may already be up.
$runner = $null
if ($onWindows) {
    $runner = [IO.Path]::ChangeExtension([IO.Path]::GetTempFileName(), '.cmd')
    Set-Content -Path $runner -Encoding ASCII -Value @(
        '@echo off',
        "start `"`" /affinity $Affinity /b /wait dotnet test `"$project`" --settings `"$runsettings`" --no-build $filter"
    )
}
else {
    # taskset wants a core list rather than a mask
    $mask = [Convert]::ToInt64($Affinity, 16)
    $cores = (0..63 | Where-Object { $mask -band ([int64]1 -shl $_) }) -join ','
    if (-not (Get-Command taskset -ErrorAction SilentlyContinue)) {
        Write-Warning 'no taskset, running unpinned - the flickers likely will not reproduce'
        $cores = $null
    }
}

$label = if ($Component) { $Component } else { 'all components' }
Write-Host "$label x$Runs on cpu mask $Affinity"

$signatures = 'mismatch after setting', 'did not fire', 'Timeout \d+ms', 'Exception :'
$fails = 0

for ($i = 1; $i -le $Runs; $i++) {
    if ($onWindows) {
        $out = & cmd.exe /c $runner 2>&1 | Out-String
    }
    elseif ($cores) {
        $out = & taskset -c $cores dotnet test $project --settings $runsettings --no-build $(if ($Component) { '--filter'; "FullyQualifiedName~$Component" }) 2>&1 | Out-String
    }
    else {
        $out = & dotnet test $project --settings $runsettings --no-build $(if ($Component) { '--filter'; "FullyQualifiedName~$Component" }) 2>&1 | Out-String
    }

    if ($out -match 'Failed!') {
        $fails++
        Write-Host "run $i`: FAIL"
        $out -split "`r?`n" |
            Select-String -Pattern $signatures |
            ForEach-Object { $_.Line.Trim() } |
            Select-Object -Unique -First 5 |
            ForEach-Object { Write-Host "  $_" }
    }
    else {
        Write-Host -NoNewline '.'
    }
}

Write-Host ''
Write-Host "$fails/$Runs runs failed"

if ($runner) { Remove-Item $runner -ErrorAction SilentlyContinue }
if ($fails -gt 0) { exit 1 }
