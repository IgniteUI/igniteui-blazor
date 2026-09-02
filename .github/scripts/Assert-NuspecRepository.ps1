# Asserts a packed NuGet package carries the provenance metadata consumers rely on.
# The nuspec is generated from MSBuild properties at pack time, so an unset one yields a package that
# restores fine but cannot be traced to source -- 0.1.1 shipped a commit with no repository url.
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string]$PackagePath,

    [Parameter(Mandatory)]
    [string]$ExpectedRepositoryUrl,

    [Parameter(Mandatory)]
    [string]$ExpectedCommit,

    [string]$ExpectedPackageId,

    [string]$ExpectedVersion
)

$ErrorActionPreference = 'Stop'

if (-not (Test-Path -LiteralPath $PackagePath -PathType Leaf)) {
    throw "NuGet package not found: $PackagePath"
}

if ($ExpectedCommit -notmatch '^[0-9a-fA-F]{40}$') {
    throw "ExpectedCommit must be a full 40-character git SHA, but was '$ExpectedCommit'."
}

Add-Type -AssemblyName System.IO.Compression.FileSystem

$archive = [System.IO.Compression.ZipFile]::OpenRead((Resolve-Path -LiteralPath $PackagePath).ProviderPath)
try {
    $entry = $archive.Entries |
        Where-Object { $_.FullName -notlike '*/*' -and $_.FullName -like '*.nuspec' } |
        Select-Object -First 1

    if ($null -eq $entry) {
        throw "No .nuspec found at the root of $PackagePath."
    }

    $reader = New-Object System.IO.StreamReader($entry.Open())
    try {
        $nuspecXml = $reader.ReadToEnd()
    }
    finally {
        $reader.Dispose()
    }
}
finally {
    $archive.Dispose()
}

$document = New-Object System.Xml.XmlDocument
$document.PreserveWhitespace = $false
$document.LoadXml($nuspecXml)

# The nuspec default namespace changes with the schema version, so match on local names only.
$metadata = $document.SelectSingleNode('/*[local-name()="package"]/*[local-name()="metadata"]')
if ($null -eq $metadata) {
    throw "The nuspec in $PackagePath has no <metadata> element."
}

function Get-MetadataValue([string]$Name) {
    $node = $metadata.SelectSingleNode("*[local-name()=`"$Name`"]")
    if ($null -eq $node) { return $null }
    return $node.InnerText.Trim()
}

$problems = @()

function Assert-Value([string]$Label, [string]$Actual, [string]$Expected) {
    if ([string]::IsNullOrWhiteSpace($Actual)) {
        $script:problems += "$Label is missing from the nuspec."
    }
    elseif ($Expected -and $Actual -ne $Expected) {
        $script:problems += "$Label is '$Actual', expected '$Expected'."
    }
}

$repository = $metadata.SelectSingleNode('*[local-name()="repository"]')
if ($null -eq $repository) {
    $problems += '<repository> is missing from the nuspec.'
}
else {
    Assert-Value 'repository/@type' $repository.GetAttribute('type') 'git'
    Assert-Value 'repository/@url' $repository.GetAttribute('url') $ExpectedRepositoryUrl
    Assert-Value 'repository/@commit' $repository.GetAttribute('commit') $ExpectedCommit
}

Assert-Value 'authors' (Get-MetadataValue 'authors') $null
Assert-Value 'projectUrl' (Get-MetadataValue 'projectUrl') $null
Assert-Value 'description' (Get-MetadataValue 'description') $null

if ($ExpectedPackageId) {
    Assert-Value 'id' (Get-MetadataValue 'id') $ExpectedPackageId
}

if ($ExpectedVersion) {
    Assert-Value 'version' (Get-MetadataValue 'version') $ExpectedVersion
}

$license = $metadata.SelectSingleNode('*[local-name()="license"]')
if ($null -eq $license) {
    $problems += '<license> is missing from the nuspec.'
}
elseif ($license.GetAttribute('type') -ne 'expression') {
    $problems += "license/@type is '$($license.GetAttribute('type'))', expected 'expression'."
}

# 'authors' defaults to the assembly name when <Authors> is unset, which is not an author.
$authors = Get-MetadataValue 'authors'
if ($authors -and $ExpectedPackageId -and $authors -eq $ExpectedPackageId) {
    $problems += "authors is '$authors', which is the package id rather than a real author. Set <Authors> in the project file."
}

if ($problems.Count -gt 0) {
    throw "Package provenance metadata validation failed for $([System.IO.Path]::GetFileName($PackagePath)):`n- $($problems -join "`n- ")"
}

Write-Host "Verified nuspec provenance for $([System.IO.Path]::GetFileName($PackagePath)):"
Write-Host "  repository url    : $($repository.GetAttribute('url'))"
Write-Host "  repository commit : $($repository.GetAttribute('commit'))"
Write-Host "  authors           : $authors"
