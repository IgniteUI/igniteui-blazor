<#
.SYNOPSIS
    Generates a CycloneDX SBOM for the project's npm runtime dependency tree, via cyclonedx-npm.

.DESCRIPTION
    dotnet-CycloneDX only inventories the .csproj; it has no notion of the webpack bundle or the
    igniteui-webcomponents theme CSS the .nupkg also ships. This covers that half of the shipped
    artifact so the merged document (see Merge-CycloneDxSbom.ps1) describes both ecosystems.

    cyclonedx-npm is a pinned package.json devDependency, restored by 'npm ci' like every other build
    tool here. npx only resolves a local install by walking up from the current directory, so this runs
    it from the manifest's own directory - not the caller's cwd - to avoid falling back to a registry
    fetch of an ad-hoc version.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$ManifestPath,

    [Parameter(Mandatory)]
    [string]$OutputFile,

    [string]$SpecVersion = '1.6'
)

$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if (-not (Test-Path -LiteralPath $ManifestPath -PathType Leaf)) {
    throw "npm package manifest not found: $ManifestPath"
}

$outputDirectory = Split-Path -Path $OutputFile -Parent
if ($outputDirectory) {
    New-Item -ItemType Directory -Path $outputDirectory -Force | Out-Null
}

$resolvedOutputFile = [System.IO.Path]::GetFullPath($OutputFile)
$projectDirectory = Split-Path -Path (Resolve-Path -LiteralPath $ManifestPath) -Parent

Push-Location -LiteralPath $projectDirectory
try {
    # --omit dev excludes webpack, TypeScript and the rest of the build tooling, which is never shipped;
    # igniteui-webcomponents and lit-html are real dependencies and stay.
    npx --no-install cyclonedx-npm `
        --omit dev `
        --spec-version $SpecVersion `
        --output-format JSON `
        --output-file $resolvedOutputFile
}
finally {
    Pop-Location
}

if (-not (Test-Path -LiteralPath $OutputFile -PathType Leaf) -or (Get-Item -LiteralPath $OutputFile).Length -eq 0) {
    throw "cyclonedx-npm did not produce a document at $OutputFile."
}

$bom = Get-Content -LiteralPath $OutputFile -Raw | ConvertFrom-Json
$components = @($bom.components)

if ($components.Count -eq 0) {
    throw 'npm CycloneDX document contains no components.'
}

Write-Host "npm CycloneDX $($bom.specVersion): $($components.Count) production components."
