using Microsoft.PointOfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal static class GlobalPosPrinter
    {
        public static PosPrinter UPrinter { get; private set; }
        public static bool isInitialized => UPrinter != null;

        public static bool Initialize(string printerName ,out string errorMessage)
        {
            errorMessage = string.Empty;
            
            if(UPrinter != null)
            {
                errorMessage = "Tiskárna je již inicializována.";
                return false;
            }
            try
            {
                PosExplorer explorer = new PosExplorer();
                DeviceInfo deviceInfo = explorer.GetDevice(printerName);
                if(deviceInfo == null)
                {
                    errorMessage = $"Tiskárna nebyla nalezena.";
                    return false;
                }
                Console.WriteLine(deviceInfo.ToString());
                UPrinter = (PosPrinter)explorer.CreateInstance(deviceInfo);
                UPrinter.Open();
                UPrinter.Claim(1000);
                UPrinter.DeviceEnabled = true;
                UPrinter.RecLineChars = 48;
            }
            catch(Exception ex)
            {
                errorMessage = $"Chyba při inicializaci tiskárny: {ex.Message}";
                return false;
            }

            return true;
        }

        public static void Dispose()
        {
            if(UPrinter != null)
            {
                UPrinter.DeviceEnabled = false;
                UPrinter.Release();
                UPrinter.Close();
                UPrinter = null;
            }
        }

        public static void PrintSeparator(Alignment alignment = Alignment.Left)
        {
            if (UPrinter == null)
            {
                throw new InvalidOperationException("Tiskárna není inicializována.");
            }

            if (!UPrinter.DeviceEnabled)
            {
                throw new InvalidOperationException("Tiskárna není povolena.");
            }

            // Určení maximální šířky řádku na základě vlastnosti RecLineChars
            int lineWidth = UPrinter.RecLineChars > 0 ? UPrinter.RecLineChars : 48; // Pokud není známé, použije 40
            string separator = new string('-', lineWidth);

            // Zarovnání separátoru
            string alignedSeparator = UAlignText(separator, alignment);

            // Tisk oddělovače
            UPrinter.PrintNormal(PrinterStation.Receipt, alignedSeparator + "\r\n");
        }

        public static void PrintPricedItem(string text, string price)
        {
            UPrinter.PrintNormal(PrinterStation.Receipt, text);
            UPrinter.PrintNormal(PrinterStation.Receipt, UAlignText(price, Alignment.Right) + "\r\n");
        }

        private static string UAlignText(string text, Alignment alignment)
        {
            int lineWidth = UPrinter.RecLineChars > 0 ? UPrinter.RecLineChars : 40; // Výchozí šířka je 40

            switch (alignment)
            {
                case Alignment.Left:
                    return text.PadRight(lineWidth); // Zarovnání vlevo
                case Alignment.Center:
                    int padding = (lineWidth - text.Length) / 2;
                    return text.PadLeft(padding + text.Length).PadRight(lineWidth); // Zarovnání na střed
                case Alignment.Right:
                    return text.PadLeft(lineWidth); // Zarovnání vpravo
                default:
                    return text;
            }
        }

        public static void PrintPayment(Payments payment, int price, int tendered)
        {
            PrintSeparator();
            PrintPricedItem("Celkem", $"{price}Kč");
            switch (payment)
            {
                case Payments.Cash:
                    PrintPricedItem("Hotově", $"{price}");
                    PrintPricedItem("Vráceno", $"{tendered}");
                    break;
                case Payments.Card:
                    PrintPricedItem("Karta", $"{price}");
                    break;
            }
        }
    }

    public enum Alignment
    {
        Left,
        Center,
        Right
    }

    public enum PrinterTypes
    {
        OPOS,
        ESC
    }

    public enum Payments
    {
        Cash,
        Card
    }
}
