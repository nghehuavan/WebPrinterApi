using System.Drawing.Printing;

namespace KikanPrinter.Helper
{
    public static class PrintHelper
    {
        public static void PrintPdf(string printerName, string filePath)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            printerSettings.PrinterName = printerName;
            printerSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            PdfiumViewer.PdfDocument pdfiumDoc = PdfiumViewer.PdfDocument.Load(filePath);
            PrintDocument pd = pdfiumDoc.CreatePrintDocument();
            pd.PrinterSettings = printerSettings;
            pd.Print();
        }
    }
}
