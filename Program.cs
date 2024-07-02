using System.Runtime.InteropServices;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.Security;

namespace PDFPasswordRemover
{
    class Program
    {
        static void WriteColor(ConsoleColor color, string msg, string fp, string exptn = "")
        {
            var colfp = ConsoleColor.DarkGray;
            var colexptn = ConsoleColor.DarkYellow;

            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = colfp;
            Console.Write(fp);
            if (!string.IsNullOrEmpty(exptn))
            {
                Console.ForegroundColor = colexptn;
                Console.Write(" (Exception Details: " + exptn + ")");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            // Argument Handling
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Usage: RemovePdfPasswords.exe <folder_path> [-o]");

                Console.WriteLine("All PDF files processed!");
                Console.WriteLine("Press ENTER key to continue...");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                // Console.ReadKey(true)
                return;
            }

            string folderPath = args[0];
            bool overwrite = args.Length == 2 && args[1] == "-o";
            string[] filePaths = Directory.GetFiles(folderPath, "*");

            // Prompt for Password
            Console.Write("Enter the password for the PDF files: ");
            string password = Console.ReadLine() ?? ""; // Handle null input

            //overwrite = true
            // Output Mode Information
            if (overwrite)
            {
                Console.WriteLine("Overwrite mode ENABLED. Existing decrypted files will be overwritten.\n");
            }
            else
            {
                Console.WriteLine("Overwrite mode DISABLED. Files will be prefixed with '_decrypted'.\n");
            }

            // Process Each File
            foreach (string filePath in filePaths)
            {
                try
                {
                    if (filePath.ToLower().EndsWith(".pdf"))
                    {

                        var isEncrypted = true;

                        // Opening the document with invalid password to check if it is encrypted at all; if it opens it is not encrypted
                        try
                        {
                            var pdfDocTest = PdfReader.Open(filePath, "Invalidpassword") as PdfDocument;
                            isEncrypted = false;
                        }
                        catch { }

                        if (isEncrypted)
                        {
                            // Open PDF using PdfSharpCore
                            using (var pdfDocImport = PdfReader.Open(filePath, password, PdfDocumentOpenMode.Import))
                            {
                                // Create a new document
                                using (var document = new PdfDocument())
                                {
                                    // Import pages into the new document
                                    foreach (var page in pdfDocImport.Pages)
                                    {
                                        document.AddPage(page);
                                    }

                                    // Remove security settings
                                    document.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.None;
                                    // Additional permissions can be set as needed
                                    document.SecuritySettings.PermitAccessibilityExtractContent = true;
                                    document.SecuritySettings.PermitAnnotations = true;
                                    document.SecuritySettings.PermitAssembleDocument = true;
                                    document.SecuritySettings.PermitExtractContent = true;
                                    document.SecuritySettings.PermitFormsFill = true;
                                    document.SecuritySettings.PermitFullQualityPrint = true;
                                    document.SecuritySettings.PermitModifyDocument = true;
                                    document.SecuritySettings.PermitPrint = true;

                                    // Save Decrypted File
                                    //Suffix string newFilePath = overwrite ? filePath : Path.Combine(Path.GetDirectoryName(filePath) ?? "", Path.GetFileNameWithoutExtension(filePath) + "_decrypted.pdf");
                                    string newFilePath = overwrite ? filePath : Path.Combine(Path.GetDirectoryName(filePath) ?? "", "decrypted_" + Path.GetFileNameWithoutExtension(filePath) + ".pdf");
                                    document.Save(newFilePath);

                                    //Console.WriteLine($"SUCCESS: Removed password from: {Path.GetFileName(filePath)}");
                                    WriteColor(ConsoleColor.Green, "SUCCESS: Removed password from: ", Path.GetFileName(filePath));
                                }
                            }
                        }
                        else
                        {
                            // PDF is not password protected
                            //Console.WriteLine($"SKIPPING: File is not password-protected: {Path.GetFileName(filePath)}");
                            WriteColor(ConsoleColor.DarkCyan, "SKIPPING: File is not password-protected: ", Path.GetFileName(filePath));

                        }

                    }
                    else
                    {
                        //Console.WriteLine($"SKIPPING: File is not a PDF: {Path.GetFileName(filePath)}");
                        WriteColor(ConsoleColor.DarkCyan, "SKIPPING: File is not a PDF: ", Path.GetFileName(filePath));
                    }
                }
                catch (PdfReaderException ex)
                {
                    if (ex.Message.Contains("password is invalid"))
                    {
                        //Console.WriteLine($"ERROR: Incorrect password for: {Path.GetFileName(filePath)}");
                        WriteColor(ConsoleColor.Red, "ERROR: Incorrect password for: ", Path.GetFileName(filePath));
                    }
                    else
                    {
                        //Console.WriteLine($"ERROR: Unsupported security settings for: {Path.GetFileName(filePath)} (Details: {ex.Message})");
                        var cleanedMessage = ex.Message;
                        cleanedMessage = cleanedMessage.Replace("If you think this is a bug in PDFsharp, please send us your PDF file.", "").TrimEnd();
                        WriteColor(ConsoleColor.DarkRed, "UNKNOWN ERROR: ", Path.GetFileName(filePath), cleanedMessage);
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"ERROR: An error occurred while processing {Path.GetFileName(filePath)}: {ex.Message}");
                    var cleanedMessage = ex.Message;
                    cleanedMessage = cleanedMessage.Replace("If you think this is a bug in PDFsharp, please send us your PDF file.", "").TrimEnd();
                    WriteColor(ConsoleColor.DarkRed, "UNKNOWN ERROR: ", Path.GetFileName(filePath), cleanedMessage);
                }

                Console.ResetColor();
            }

            Console.WriteLine("All PDF files processed!");
            Console.WriteLine("Press ENTER key to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }
    }
}