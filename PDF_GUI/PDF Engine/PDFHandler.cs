using System;
using System.IO;
using System.Linq;
using System.Windows;
using PDF_GUI.Helpers;

namespace PDF_GUI.PDF_Engine
{
    class PdfHandler
    {
        private readonly HelperMethods _helperMethods = new HelperMethods();
        private readonly PdfConvertHandler _pdfConvertHandler = new PdfConvertHandler();
        private readonly LogHandler _loghandler = new LogHandler();

        public void ConvertFilesToPdf(string sourcePath, string targetPath, Action action)
        {
            if (!string.IsNullOrEmpty(sourcePath) && !string.IsNullOrEmpty(targetPath))
            {
                try
                {
                    var listOfFiles = Directory.GetFiles(sourcePath);
                    foreach (var file in listOfFiles)
                    {
                        var ext = _helperMethods.GetExtension(file);
                        string pathTo = targetPath + "/" + ext.First() + ".pdf";
                        if(!File.Exists(pathTo)) _pdfConvertHandler.ReturnPdfFilePath(ext.Last(), file, pathTo);
                    }
                    action.Invoke();
                    _loghandler.WriteToLog(listOfFiles.Length);
                    MessageBox.Show("File converted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The program failed with error code: " + Environment.NewLine + ex);
                }
            }
            else MessageBox.Show("Both source and destination/target path must be filled in!!");
        }

        public void ConvertFileToPdf(string sourcePath, string targetPath, Action action)
        {
            if (!string.IsNullOrEmpty(sourcePath) && !string.IsNullOrEmpty(targetPath))
            {
                try
                {
                    var ext = _helperMethods.GetExtension(sourcePath);
                    string pathTo = targetPath + "/" + ext.First() + ".pdf";
                    _pdfConvertHandler.ReturnPdfFilePath(ext.Last(), sourcePath, pathTo);
                    action.Invoke();
                    _loghandler.WriteToLog(1);
                    MessageBox.Show("File converted successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The program failed with error code: " + Environment.NewLine + ex);
                }
            }
            else MessageBox.Show("Both source and destination/target path must be filled in!!");
        }
    }
}
