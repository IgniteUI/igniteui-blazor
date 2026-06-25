#!/usr/bin/env pwsh
#
# test-templates.ps1
#
# Smoke-tests the IgniteUI.Blazor.Templates package by:
#   1. Installing the templates from this source folder.
#   2. Scaffolding a fresh Blazor *Server* project under ./tmp via `dotnet new igb-blazor`.
#   3. Generating every item template (igb-*) into the project's Components/Pages folder.
#   4. Wiring each generated page into Components/Layout/NavMenu.razor as an IgbNavDrawerItem.
#
# Usage:  ./test-templates.ps1
#
# Parameters (or matching env vars for parity with test-templates.sh):
#   -ProjectName   name of the generated project          (default: IgbTemplateTest)
#   -NavIcon       material-icons name used for nav items  (default: widgets)
#   -SkipRestore   skip NuGet restore on create            (default: $true)

[CmdletBinding()]
param(
    [string]$ProjectName = $(if ($env:PROJECT_NAME) { $env:PROJECT_NAME } else { 'IgbTemplateTest' }),
    [string]$NavIcon     = $(if ($env:NAV_ICON)     { $env:NAV_ICON }     else { 'widgets' }),
    [bool]  $SkipRestore = $(if ($env:SKIP_RESTORE) { $env:SKIP_RESTORE -eq '1' } else { $true })
)

$ErrorActionPreference = 'Stop'

$ScriptDir = $PSScriptRoot
$TmpDir    = Join-Path $ScriptDir 'tmp'
$ItemsDir  = Join-Path $ScriptDir 'templates/item'

Write-Host "==> Cleaning $TmpDir"
if (Test-Path $TmpDir) {
    Remove-Item -Recurse -Force $TmpDir
}
New-Item -ItemType Directory -Path $TmpDir | Out-Null

Write-Host "==> Installing templates from $ScriptDir"
dotnet new install $ScriptDir --force
if ($LASTEXITCODE -ne 0) { throw "dotnet new install failed" }

Write-Host "==> Creating Blazor Server project '$ProjectName'"
$createArgs = @('igb-blazor', '-n', $ProjectName, '-o', (Join-Path $TmpDir $ProjectName), '--Hosting', 'Server')
if ($SkipRestore) {
    $createArgs += '--SkipRestore'
}
dotnet new @createArgs
if ($LASTEXITCODE -ne 0) { throw "dotnet new igb-blazor failed" }

# Locate the generated app folder via its NavMenu (robust to the project sourceName rename).
$navMenu = Get-ChildItem -Path (Join-Path $TmpDir $ProjectName) -Recurse -Filter 'NavMenu.razor' |
    Where-Object { $_.FullName -replace '\\', '/' -like '*/Components/Layout/NavMenu.razor' } |
    Select-Object -First 1 -ExpandProperty FullName
if (-not $navMenu) {
    Write-Error "ERROR: NavMenu.razor not found in generated project."
    exit 1
}
# .../Components/Layout -> app dir
$appDir   = Split-Path (Split-Path (Split-Path $navMenu -Parent) -Parent) -Parent
$pagesDir = Join-Path $appDir 'Components/Pages'
Write-Host "==> App dir:   $appDir"
Write-Host "==> Pages dir: $pagesDir"
Write-Host "==> NavMenu:   $navMenu"

# Generate every item template into Components/Pages and collect a nav snippet for each.
$navSnippet = ''
Write-Host "==> Generating item templates"
foreach ($d in Get-ChildItem -Path $ItemsDir -Directory) {
    $tj = Join-Path $d.FullName '.template.config/template.json'
    if (-not (Test-Path $tj)) { continue }

    $json   = Get-Content -Raw $tj | ConvertFrom-Json
    $short  = $json.shortName
    $source = $json.sourceName
    Write-Host "    - $source  (dotnet new $short)"
    dotnet new $short -n $source -o $pagesDir
    if ($LASTEXITCODE -ne 0) { throw "dotnet new $short failed" }

    $navSnippet += "<IgbNavDrawerItem Active=`"@IsActive(`"$source`")`" @onclick=@(() => Nav.NavigateTo(`"$source`"))>`n"
    $navSnippet += "    <span slot=`"icon`">`n"
    $navSnippet += "        <span class=`"material-icons icon`">`n"
    $navSnippet += "            $NavIcon`n"
    $navSnippet += "        </span>`n"
    $navSnippet += "    </span>`n"
    $navSnippet += "    <div slot=`"content`">$source</div>`n"
    $navSnippet += "</IgbNavDrawerItem>`n"
}

# Insert the collected nav items right before the @code block (i.e. after the existing nav items).
Write-Host "==> Updating NavMenu.razor"
$lines  = Get-Content $navMenu
$output = New-Object System.Collections.Generic.List[string]
$done   = $false
foreach ($line in $lines) {
    if (-not $done -and $line -match '^@code') {
        $output.Add($navSnippet.TrimEnd("`n"))
        $done = $true
    }
    $output.Add($line)
}
Set-Content -Path $navMenu -Value $output

Write-Host "==> Done."
Write-Host "    Project: $(Join-Path $TmpDir $ProjectName)"
Write-Host "    To run:  dotnet run --project `"$appDir`""
