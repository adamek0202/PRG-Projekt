using ESC_POS_USB_NET.Printer;
using System;

namespace Projekt
{
    internal static class GlobalPosPrinter
    {
        public static Printer EPrinter;

        public static int receiptId;

        public static bool InitESCPrinter(string printerName)
        {
            try
            {
                EPrinter = new Printer(printerName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při tiskárny: {ex.Message}");
                return false;
            }
        }

        public static bool InitPrinter(string printerName, out string errorMsg)
        {
            errorMsg = "";
            return InitESCPrinter(printerName);
        }

        public static string FormatTwoColumns(string leftText, string rightText, int totalWidth)
        {
            // Oříznutí textu, pokud je příliš dlouhý
            if (leftText.Length + rightText.Length > totalWidth)
            {
                leftText = leftText.Substring(0, totalWidth - rightText.Length - 1);
            }

            // Počet mezer mezi texty
            int spaces = totalWidth - leftText.Length - rightText.Length;
            if (spaces < 0) spaces = 0;

            // Sestavení řádku
            return leftText + new string(' ', spaces) + rightText;
        }

        public static void PrintPayment(Payments payment, int price, int tendered)
        {
            EPrinter.Separator();
            EPrinter.Append(FormatTwoColumns("Základ", $" {price - ((double)12 / 100 * price)}Kč", 48));
            EPrinter.Append(FormatTwoColumns("DPH 12%", $"{(double)12 / 100 * price}Kč", 48));
            EPrinter.Separator();
            EPrinter.Append(FormatTwoColumns("Celkem", $"{price}Kč", 48));
            EPrinter.Separator('=');
            if (payment == Payments.Cash)
            {
                EPrinter.Append(FormatTwoColumns("Hotově", $"{price}Kč", 48));
                EPrinter.Append(FormatTwoColumns("Vráceno", $"{tendered}Kč", 48));
            }
            else
            {
                EPrinter.Append(FormatTwoColumns("Karta", $"{price}Kč", 48));
            }
            EPrinter.Separator();
        }
    }

    public enum Payments
    {
        Cash,
        Card
    }
}
