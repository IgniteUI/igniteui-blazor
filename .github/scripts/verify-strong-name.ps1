# Verifies assemblies are strong-name signed with the approved Infragistics key.
# 'sn.exe -vf' only proves a strong name is internally consistent, so any valid private key passes;
# this also compares each assembly's public key against the value pinned in the repository.
[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [string[]]$Path,

    [Parameter(Mandatory)]
    [string]$ExpectedPublicKeyPath,

    [string]$SnPath
)

$ErrorActionPreference = 'Stop'
# Failures are aggregated per assembly, so sn.exe exit codes must not throw on their own.
$PSNativeCommandUseErrorActionPreference = $false

function ConvertTo-HexString([byte[]]$Bytes) {
    return (-join ($Bytes | ForEach-Object { $_.ToString('x2') }))
}

if (-not (Test-Path -LiteralPath $ExpectedPublicKeyPath)) {
    throw "Pinned public key file not found: $ExpectedPublicKeyPath"
}

$hexLines = @(
    Get-Content -LiteralPath $ExpectedPublicKeyPath |
        ForEach-Object { $_.Trim() } |
        Where-Object { $_ -and -not $_.StartsWith('#') }
)

# A blank or malformed pin must fail loudly; otherwise the whole check silently becomes a no-op.
if ($hexLines.Count -ne 1) {
    throw "$ExpectedPublicKeyPath must contain exactly one non-comment line, but contains $($hexLines.Count)."
}

$expectedPublicKeyHex = $hexLines[0].ToLowerInvariant()
if ($expectedPublicKeyHex -notmatch '^[0-9a-f]{320,}$' -or $expectedPublicKeyHex.Length % 2 -ne 0) {
    throw "$ExpectedPublicKeyPath does not hold a public key blob (expected an even number of at least 320 hex characters)."
}

$expectedPublicKey = [byte[]]::new($expectedPublicKeyHex.Length / 2)
for ($index = 0; $index -lt $expectedPublicKey.Length; $index++) {
    $expectedPublicKey[$index] = [Convert]::ToByte($expectedPublicKeyHex.Substring($index * 2, 2), 16)
}

# SHA-1 is not a security choice here; it is the algorithm that defines a strong-name token.
$digest = [System.Security.Cryptography.SHA1]::Create().ComputeHash($expectedPublicKey)
$tokenBytes = $digest[-8..-1]
[array]::Reverse($tokenBytes)
$expectedToken = ConvertTo-HexString $tokenBytes

if ($SnPath) {
    if (-not (Test-Path -LiteralPath $SnPath -PathType Leaf)) {
        throw "The specified sn.exe path does not exist: $SnPath"
    }

    $strongNameTool = Get-Item -LiteralPath $SnPath
}
else {
    $strongNameCommand = Get-Command sn.exe -CommandType Application -ErrorAction SilentlyContinue |
        Select-Object -First 1

    if ($null -ne $strongNameCommand) {
        $strongNameTool = Get-Item -LiteralPath $strongNameCommand.Path
    }
    else {
        $windowsSdkRoot = Join-Path ${env:ProgramFiles(x86)} 'Microsoft SDKs\Windows'
        $strongNameTool = Get-ChildItem -Path $windowsSdkRoot -Filter 'sn.exe' -Recurse -ErrorAction SilentlyContinue |
            Sort-Object -Property @{
                Expression = {
                    $match = [regex]::Match($_.FullName, '\\v(?<version>\d+(?:\.\d+)*)A?\\', 'IgnoreCase')
                    if ($match.Success) { [version]$match.Groups['version'].Value } else { [version]'0.0' }
                }
                Descending = $true
            }, @{
                Expression = { $_.FullName }
                Descending = $true
            } |
            Select-Object -First 1
    }
}

if ($null -eq $strongNameTool) {
    throw 'Could not find sn.exe on PATH or under the Windows SDK directory. Pass -SnPath explicitly.'
}

Write-Verbose "Using sn.exe from '$($strongNameTool.FullName)'."

$assemblies = @(Get-ChildItem -Path $Path -Filter '*.dll' -Recurse -File)
if ($assemblies.Count -eq 0) {
    throw "No assemblies were found under '$($Path -join ', ')'. Refusing to report success."
}

$problems = @()
foreach ($assembly in $assemblies) {
    $output = & $strongNameTool.FullName -vf $assembly.FullName
    if ($LASTEXITCODE -ne 0) {
        $problems += "$($assembly.FullName): strong-name verification failed. $(($output | Where-Object { $_ }) -join ' ')"
        continue
    }

    $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($assembly.FullName)
    $token = $assemblyName.GetPublicKeyToken()
    if ($null -eq $token -or $token.Length -eq 0) {
        $problems += "$($assembly.FullName): not strong named."
        continue
    }

    $actualToken = ConvertTo-HexString $token
    if ($actualToken -ne $expectedToken) {
        $problems += "$($assembly.FullName): public key token is $actualToken, expected $expectedToken."
        continue
    }

    # Best effort: the token is a truncated hash, so compare the whole key when it is available.
    $publicKey = $assemblyName.GetPublicKey()
    if ($null -ne $publicKey -and $publicKey.Length -gt 0) {
        $actualPublicKey = ConvertTo-HexString $publicKey
        if ($actualPublicKey -ne $expectedPublicKeyHex) {
            $problems += "$($assembly.FullName): public key does not match $ExpectedPublicKeyPath despite a matching token."
        }
    }
}

if ($problems.Count -gt 0) {
    throw "Strong-name validation failed:`n$($problems -join "`n")"
}

Write-Host "Verified $($assemblies.Count) assemblies against public key token $expectedToken."
