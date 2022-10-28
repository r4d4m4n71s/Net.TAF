# Evaluates if there was changes into the repository since the last success 
# if it's the case retrieves the root folder name and stores in a $(FolderUpdated) variable
#https://stackoverflow.com/questions/53227343/triggering-azure-devops-builds-based-on-changes-to-sub-folders
#https://learn.microsoft.com/en-us/rest/api/azure/devops/build/latest/get?view=azure-devops-rest-6.1

[CmdletBinding()]
param (
    # Build [id or name] who's pushing changes to the repo, target branch to look up
    [Parameter(Mandatory=$true,Position=1)][alias("definition-lookup")][string]$buidDefinitionLookUp,
    [Parameter(Mandatory=$true,Position=2)][alias("branch-name")][string]$branch,  
    
    # Folder to look up assemblies
    [Parameter(Mandatory=$true,Position=3)][alias("folder-lookup")][string]$folderLookup="./",
    
    # Included and excluded search criteria
    [Parameter(Mandatory=$false,Position=4)][alias("projectLike-wildcard")][string]$projectLikeWildcard="*",
    [Parameter(Mandatory=$false,Position=5)][alias("projectNotLike-wildcard")][string]$projectNotLikeWildcard="")

# Query info of the latest build exec
$url = "$($env:SYSTEM_TEAMFOUNDATIONCOLLECTIONURI)$env:SYSTEM_TEAMPROJECTID/_apis/build/latest/${buidDefinitionLookUp}?branchName=${branch}"
           
Write-Host "Latest get URL end point: $url"

$commitId ='none'
try{

    $response = Invoke-RestMethod -Uri $url -Headers @{Authorization = "Bearer $env:SYSTEM_ACCESSTOKEN"}
    Write-Host "StatusCode:"$response.StatusCode.value__   
    Write-Host "Last build source version: "$response.sourceVersion
    # id of the las commit
    $commitId = $response.sourceVersion

}catch{
    Write-Host "Error.. StatusCode:" $_.Exception.Response.StatusCode.value__ 
    Write-Host "Error.. StatusDescription:" $_.Exception.Response.StatusDescription
    throw $_.Exception
}

# Get the (git diff) between the build and the repository
$editedFiles = git diff HEAD "$commitId" --name-only

$rootFolders = @()
$editedFiles | ForEach-Object { 
    # look up root folder into
    $sepIndex = $_.IndexOf('/')
    if($sepIndex -gt 0) {
        $rootFolders += $_.substring(0, $sepIndex)
    }
}
$rootFolders = $rootFolders | select -Unique
Write-Host "Edited folders: "$rootFolders

# match folder diferences between [repo] and [build latest commit]
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

# set matched folder to global variable    
Write-Host "##vso[task.setvariable variable=SourceCodeUpdated]"$matches
Write-Host $env:ProjectsUpdated