﻿using System.Drawing.Printing;
using RawPrint;

namespace WebPrinterApi.Helper
{
    public static class PrintHelper
    {
        public static void PrintPdf(string printerName, string filePath)
        {
            IPrinter printer = new Printer();
            printer.PrintRawFile(printerName, filePath);
        }
    }
}
