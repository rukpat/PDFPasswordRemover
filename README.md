**Remove PDF Passwords**

A simple yet effective tool to remove passwords from multiple PDF files withi= n a folder. Streamlines the process of decrypting password-protected PDFs, especially when dealing with numerous files.

**Features**

* **Batch Processing:** Quickly decrypt all password-protected PDFs in a specified directory.
* **Customizable Output:** Choose to overwrite original files or create new files with the "_decrypted" prefix.
* **User-Friendly:** Simple installation process and an intuitive context menu integration with Windows Explore= r.
* **Secure:** Password is not stored after executi= on.
* **Error Handling:** Provides informati= ve messages for incorrect passwords or unsupported security settings.

**Requirements**

* **Windows:**= This tool is designed for Windows and requires the .NET 8.0 runtime.
* **PDFsharpCore** **Library:** This library is used for PDF manipulation. It is automatically installed during the build process.&lt;= o:p&gt;

**Installation**

NaN.**Download: Download the latest release (PDFPasswordRemoverWinInstaller.exe) from the Releases page.**
*** **Install:** Double-click the installer and follow the on-screen instructions. The installer will automatically create a context menu entry in Windows Explorer.**

**

**Usage**

NaN.**Right-Click:=** Right-click on a folder containing PDF files.
NaN.**Select:** Choose "Remove PDF Passwords&qu= ot; from the context menu.
NaN.**Enter Password:** Enter the password= to decrypt the PDFs.

**Options**

The installer will provide options to:

* **Overwrite Files:** Choose to overwrite original files with the decrypted versions= .
* **Create New Files:** If you don't want to overwrite, new files with the suffix "_decrypted" will be created.

**Command Line Usage (Optional)**

After installation, you can optionally use the command line for more control:

Bash

PDFPasswordRemover.exe <folder_path\> &lt;password&gt; \[-o\]

&n= bsp;

Use code with caution.

content_copy

* <folder_path>: The path to the folder containing the PDF files.
* &lt;password&gt;: The password used to encrypt the PDFs.
* -o (Optional): Use this flag to overwrite the original files with= the decrypted versions.

**Disclaimer**

* This tool is intended for legal use o= nly. Use it responsibly and respect copyright laws.
* The author is not responsible for any misuse or damage caused by this software.

**Contributing**

Contributions are welcome! Please feel free to submit bug reports, feature requests, or p= ull requests.

**License**

This project is licensed under the MIT License. See the LICENSE file for details.

**
