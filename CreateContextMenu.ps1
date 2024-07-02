$exePath = "D:\OneDrive\0RPPersonal Dev Projects\Utilities\PDFPasswordRemover\bin\Release\net8.0\PDFPasswordRemover.exe"  # Replace with the actual path
$iconPath = "D:\OneDrive\0RPPersonal Dev Projects\Utilities\PDFPasswordRemover\bin\pdf_broken_chain.ico" # Replace with the actual icon path
$shellKey = "HKEY_CLASSES_ROOT\Directory\shell\Remove PDF Passwords"
$commandKey = "$shellKey\command"

# Create keys and set values in the registry
New-Item -Path $shellKey -Force | Out-Null
New-Item -Path $commandKey -Force | Out-Null
Set-ItemProperty -Path $commandKey -Name "(Default)" -Value ('"' + $exePath + '" "%1" "%2"')
Set-ItemProperty -Path $shellKey -Name "Icon" -Value $iconPath



#Computer\HKEY_CLASSES_ROOT\Directory\shell\RemovePDFPasswords