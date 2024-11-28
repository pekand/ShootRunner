$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location (Join-Path $scriptDir "..")

$tag = git describe --tags --abbrev=0
Write-Output "TAG=>$tag<"

$innoInstallPath = 'iscc'
& $innoInstallPath /q install.iss -dAppVersion=%TAG%

$CERT_CODE = $env:CERT_CODE
Write-Host "CERT_CODE=>$CERT_CODE<"
$CERT_PWD = $env:CERT_PWD
Write-Host "CERT_PWD=>$CERT_PWD<"


$TARGET1="output\ShootRunner_v1.0.0.exe"
$signtoolPath = 'signtool.exe'
& $signtoolPath sign /fd SHA256 /f "$CERT_CODE" /p $CERT_PWD /tr http://timestamp.digicert.com /td sha256 /v "$TARGET1"
& $signtoolPath verify /pa /v "$TARGET1"

$hash = Get-FileHash -Path "output\ShootRunner_v1.0.0.exe" -Algorithm SHA256
$hash.Hash | Out-File -FilePath "output\ShootRunner_v1.0.0.SHA256"

Write-Host "Press any key to continue..."
$null = Read-Host