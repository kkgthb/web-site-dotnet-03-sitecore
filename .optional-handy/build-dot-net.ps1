Write-Host('Start folder cleanup')
Remove-Item "$PSScriptRoot\..\my_output" -Recurse -Force -ErrorAction silentlycontinue
Remove-Item "$PSScriptRoot\..\src\web\bin" -Recurse -Force -ErrorAction silentlycontinue
Remove-Item "$PSScriptRoot\..\src\web\obj" -Recurse -Force -ErrorAction silentlycontinue
Write-Host('End folder cleanup')

Write-Host('Start .NET build')
Push-Location "$PSScriptRoot\..\src\web"
[Environment]::SetEnvironmentVariable('SITECORE_INSTANCEURI', 'https://example.com', 'User')
dotnet publish --configuration Release --output ../../my_output
[Environment]::SetEnvironmentVariable('SITECORE_INSTANCEURI', '', 'User')
Pop-Location
Write-Host('End .NET build')