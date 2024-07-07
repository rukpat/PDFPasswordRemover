![main image](./blob/main/Installer/PDFPasswordRemover.ico)
#  Remove PDF Passwords 

A simple and effective tool to remove passwords from multiple PDF files within a folder. 

The core program can be run independently but is more effective with the Windows Explorer's Right-click Context Menu

<insert Image>



## Features

* **Batch Processing:** Quickly decrypt all password-protected PDFs in a specified directory.
* **Customizable Output:** Choose to overwrite original files or create new files with the "_decrypted" prefix.  
* **User-Friendly:** Simple installation process and an intuitive context menu integration with Windows Explorer.
* **Secure:** Password is not stored after execution.



## Requirements

- **Windows:** 
This tool is designed for Windows and requires the .NET 8.0 runtime.
- **PDFsharpCore Library:** 
This library is used for PDF manipulation. It is automatically installed by the installer.



## Installation

**Download:** 
- Download the latest release **PDFPasswordRemoverWinx64Installer.exe** from the [Releases](./releases) page.

**Install:** 
- Double-click the installer and follow the on-screen instructions. 

### What is installed?
1. The program and its dependencies in the Windows **_Program File_** or **_<Users>\AppData\Local\Programs_** directory depending on the permissions used at the time of install.
2. Creates the following registry keys for the context menu entry in Windows Explorer:
```
[HKEY_CLASSES_ROOT\Directory\shell\Remove PDF Passwords]
Icon="{app dir}\PDFPasswordRemover.ico"

[HKEY_CLASSES_ROOT\Directory\shell\Remove PDF Passwords\command]
@="{app dir}\PDFPasswordRemover.exe" "%V"
```

### Usage

**Right-Click:** 
- Right-click on a folder containing PDF files in the Windows Explorer.

**Select:** 
- Choose "Remove PDF Passwords from the context menu.

**Enter Password:** 
- Enter the password to decrypt the PDFs.


## How to compile the source code?
The core working functionality is only in the [Program.cs](Program.cs) file and the [registry keys](./blob/main/Installer/ContextMenu.reg). 

Rest of the files are for:
- Visual Studio Code - the IDE and build 
- [Inno Setup](https://jrsoftware.org/isinfo.php) - create the installer (see  [https://jrsoftware.org/isinfo.php](https://jrsoftware.org/isinfo.php) for more details)

Compile the code as follows: 
1. In Visual Studio Code open the project folder as  `And folder to Workspace..` or `Open Folder...`
2. Press: `Ctrl + Shift + P`
3. Select `Task: Run Task`
4. Select `Inno Setup: Compile Script` 
This should start the code build and the Installer creation and throw errors for an missing dependency 😄 (e.g. **PDFsharpCore Library:**) 

> [!TIP]
> Use your favourite GenAI to debug through the errors.

Installer `PDFPasswordRemoverWinInstaller.exe` file is created in the `.\Installer\Winx64Installer\` folder when it all works. 


## WARNING: Use the code with caution.

> [!IMPORTANT] 
> **Disclaimer**
> - This tool is intended for legal use only. Use it responsibly and respect copyright laws.
> - The author is not responsible for any misuse or damage caused by this software.
>
> **Contributing**
>
> Contributions are welcome! Please feel free to submit bug reports, feature requests, or p= ull requests.
>
> **License**
>
> This project is licensed under the MIT License. See the LICENSE file for details.
