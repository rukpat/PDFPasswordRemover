# Get the directory where the script is located (which is also the installation directory)
$installDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
#log in a file during install
$logfile = Join-Path $installDir "CCMrun.log" 
Start-Transcript -Path $logfile -Append

#Computer\HKEY_CLASSES_ROOT\Directory\shell\RemovePDFPasswords
$exePath = Join-Path $installDir "PDFPasswordRemover.exe"  
$iconPath = Join-Path $installDir "pdf_broken_chain.ico"


# Remove "archive" from the registry path (Important Correction!)
$shellKey = "Registry::HKEY_CLASSES_ROOT\Directory\shell\Remove PDF Passwords"
$commandKey = "$shellKey\command"

# Error handling wrapper function
function Add-ContextMenuWithLogging {
    param(
        [string]$ShellKey,
        [string]$CommandKey,
        [string]$ExePath,
        [string]$IconPath
    )

    try {
        Write-Host "Creating registry keys..."
        New-Item -Path $ShellKey -Force | Out-Null
        New-Item -Path $CommandKey -Force | Out-Null

        Write-Host "Setting command value..."
        #Set-ItemProperty -Path $CommandKey -Name "(Default)" -Value ('"' + $ExePath + '" "%V"')
        Set-ItemProperty -Path $CommandKey -Name "(Default)" -Value ('"powershell.exe" -ExecutionPolicy Bypass -File "' + $ExePath + '" "%V"')



        Write-Host "Setting icon value..."
        Set-ItemProperty -Path $ShellKey -Name "Icon" -Value $IconPath
        Write-Host "Context menu item added successfully."
    }
    catch {
        Write-Host "An error occurred while adding the context menu item:" -ForegroundColor Red
        Write-Host $_.Exception.Message -ForegroundColor Red
    }
}

# Input Validation
if (-not (Test-Path $exePath)) {
    Write-Host "Error: Executable path not found. Please check the path and try again." -ForegroundColor Red
    return
}

if (-not (Test-Path $iconPath)) {
    Write-Host "Warning: Icon path not found. The context menu item will be created without an icon." -ForegroundColor Yellow
}

# Create keys and set values in the registry (with logging and error handling)
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
Write-Host "$timestamp START ----------------------------"

Add-ContextMenuWithLogging -ShellKey $shellKey -CommandKey $commandKey -ExePath $exePath -IconPath $iconPath

$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
Write-Host "$timestamp END   ----------------------------"

# Wait for any key to be pressed before exiting
Write-Host "Press ENTER key to continue..."
if ($Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown").Key -eq 'Enter') { ... }