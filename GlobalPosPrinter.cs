using ESC_POS_USB_NET.Printer;
using System;

namespace Projekt
{
    internal static class GlobalPosPrinter
    {
        public static Printer EPrinter;

        public static int receiptId;

        public static string InitPrinter(string printerName)
        {
            try
            {
                EPrinter = new Printer(printerName);
                return "";
            }
            catch (Exception ex)
            {
                return $"Chyba při tiskárny: {ex.Message}";
            }
        }
    }
}
