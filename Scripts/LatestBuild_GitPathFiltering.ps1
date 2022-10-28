# Evaluates if there was changes into the repository since the last success 
# if it's the case retrieves the root folder name and stores in a $(FolderUpdated) variable
#https://stackoverflow.com/questions/53227343/triggering-azure-devops-builds-based-on-changes-to-sub-folders
#https://learn.microsoft.com/en-us/rest/api/azure/devops/build/latest/get?view=azure-devops-rest-6.1

[CmdletBinding()]
param (
    [Parameter(Mandatory=$true,Position=1)][alias("definition-lookup")][string]$buidDefinitionLookUp,
    [Parameter(Mandatory=$true,Position=2)][alias("branch-name")][string]$branch,  
    [Parameter(Mandatory=$true,Position=3)][alias("folder-lookup")][string]$folderLookup="./",
    [Parameter(Mandatory=$false,Position=4)][alias("projectLike-wildcard")][string]$projectLikeWildcard="*",
    [Parameter(Mandatory=$false,Position=5)][alias("projectNotLike-wildcard")][string]$projectNotLikeWildcard="")

$url = "$($env:SYSTEM_TEAMFOUNDATIONCOLLECTIONURI)$env:SYSTEM_TEAMPROJECTID/_apis/build/latest/${buidDefinitionLookUp}?branchName=${branch}"
           
Write-Host "Latest get URL end point: $url"

$commitId ='none'
try{

    $response = Invoke-RestMethod -Uri $url -Headers @{Authorization = "Bearer $env:SYSTEM_ACCESSTOKEN"}
    Write-Host "StatusCode:"$response.StatusCode.value__   
    Write-Host "Last build source version: "$response.sourceVersion
    $commitId = $response.sourceVersion

}catch{
    Write-Host "Error.. StatusCode:" $_.Exception.Response.StatusCode.value__ 
    Write-Host "Error.. StatusDescription:" $_.Exception.Response.StatusDescription
    throw $_.Exception
}

# Get the (git diff) since last build
$editedFiles = git diff HEAD "$commitId~" --name-only

$rootFolders = @()
$editedFiles | ForEach-Object { 
    # look up root folder
    $sepIndex = $_.IndexOf('/')
    if($sepIndex -gt 0) {
        $rootFolders += $_.substring(0, $sepIndex)
    }
}
$rootFolders = $rootFolders | select -Unique
Write-Host "Edited folders: "$rootFolders

# match expected folders vs found
#Set-Location $folderLookup
$matches = Get-ChildItem $folderLookup |
	Where-Object { 
        $_.PsIsContainer -and 
        $_.Name -like $projectLikeWildcard -and 
        $_.Name -notlike $projectNotLikeWildcard -and 
        $rootFolders.Contains($_.Name) }
    
if($matches -ne $null){
    $matches = [system.String]::Join(",", $matches)
}

Write-Host "Matches: "$matches
Write-Host "##vso[task.setvariable variable=SourceCodeUpdated]"$matches
Write-Host $env:ProjectsUpdated