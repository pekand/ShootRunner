# Assign parameters to variables
param (
    [string]$ConfigurationName,
    [string]$SolutionDir,
    [string]$OutDir,
    [string]$TargetPath
)

function Get-DecryptedSecret($path) { [System.Text.Encoding]::UTF8.GetString([System.Security.Cryptography.ProtectedData]::Unprotect([System.Convert]::FromBase64String(([System.IO.File]::ReadAllText($path)).Trim()), $null, 'CurrentUser')) }

if ($ConfigurationName -eq "Release") {

# Output the parameters for debugging
Write-Host "Post build:"
Write-Host "ConfigurationName=>$ConfigurationName< "
Write-Host "SolutionDir=>$SolutionDir<"
Write-Host "OutDir=>$OutDir<"
Write-Host "TargetPath=>$TargetPath<"
$CERT_CODE = $env:CERT_CODE
Write-Host "CERT_CODE=>$CERT_CODE<"
$CERT_PWD = (& Get-DecryptedSecret.ps1 -FilePath $CERT_CODE -ErrorAction SilentlyContinue) ?? ""
Write-Host "CERT_PWD=>$CERT_PWD<"
$CERT_STRONG_NAME = $env:CERT_STRONG_NAME
Write-Host "CERT_STRONG_NAME=>$CERT_STRONG_NAME<"

$TARGET1="${SolutionDir}ShootRunner\${OutDir}ShootRunner.exe"
Write-Host "TARGET1=>$TARGET1<"
$TARGET2="${SolutionDir}ShootRunner\${OutDir}ShootRunner.dll"
Write-Host "TARGET2=>$TARGET2<"

### $snPath = 'sn.exe'
### & $snPath -R "$TARGET2" $CERT_STRONG_NAME
### & $snPath -v "$TARGET2"

if (Test-Path $CERT_CODE) {

$signtoolPath = 'signtool.exe'
& $signtoolPath sign /fd SHA256 /f "$CERT_CODE" /p $CERT_PWD /tr http://timestamp.digicert.com /td sha256 /v "$TARGET1"
& $signtoolPath sign /fd SHA256 /f "$CERT_CODE" /p $CERT_PWD /tr http://timestamp.digicert.com /td sha256 /v "$TARGET2"
& $signtoolPath verify /pa /v "$TARGET1"
& $signtoolPath verify /pa /v "$TARGET2"

}

}