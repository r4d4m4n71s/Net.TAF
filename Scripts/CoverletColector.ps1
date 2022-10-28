# Executes coverlet console tool
# https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/GlobalTool.md

[CmdletBinding()]
param ($folderLookup,$filterTest)
$filterTag = "TestCategory=unit"

[Console]::WriteLine("folderLookup: "+$folderLookup)
[Console]::WriteLine("filterTest: "+$filterTest)

$hasMatches = $false
Get-ChildItem $folderLookup -Recurse |
    Where-Object {$_.Name -like $filterTest -and $_.FullName -NotLike "*obj*"} | Foreach-Object {
	$hasMatches = $true
	[Console]::WriteLine("Analisys matches: "+$_.FullName)
	$reportName = $_.BaseName + ".coverage.xml"
	$reportPath = $_.DirectoryName + "/"+$reportName
	[Console]::WriteLine("Report path: "+$reportPath)
	if (Test-Path $reportPath ) {
		[Console]::WriteLine("Report already exist deleting..")
        Remove-Item $reportPath -verbose
    }

	Set-Location $_.DirectoryName
	#$curDir = Get-Location 
	#[Console]::WriteLine("Current location: " + $curDir)

	$targetLibrary=$_.Name	
	coverlet $_.FullName --target "dotnet" --targetargs "test ${targetLibrary} --filter ${filterTag} --no-build" -f=opencover -o="${reportName}" --use-source-link --verbosity detailed	
}
[Console]::WriteLine("Has matches: " + ($hasMatches -ne $false))
[Console]::WriteLine("Execution result: " + $LASTEXITCODE)

if (($hasMatches -eq $false) -or ($LASTEXITCODE -ne 0)){
		throw "Unable to generate report, check folderLookup is valid and has test projects or filterTest has matches"
	}
Set-Location $folderLookup