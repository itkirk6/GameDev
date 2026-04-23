$sourceFolder = ".\Scripts"
$outputFile = ".\CombinedScripts.txt"

# Clear the output file if it already exists to prevent duplicate appending
if (Test-Path $outputFile) { Remove-Item $outputFile }

Get-ChildItem -Path $sourceFolder -Filter *.cs -Recurse | ForEach-Object {
    $header = "`n`n// " + "="*40 + "`n// FILE: $($_.Name)`n// " + "="*40 + "`n`n"
    Add-Content -Path $outputFile -Value $header
    Get-Content $_.FullName | Add-Content -Path $outputFile
}

Write-Host "All .cs files have been combined into $outputFile" -ForegroundColor Green