using ESC_POS_USB_NET.Printer;
using Microsoft.PointOfService;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Projekt
{
    internal static class GlobalPosPrinter
    {
        public static PosPrinter UPrinter { get; private set; }

        public static PrinterTypes CurrentPrinterType { get; private set; }
        public static Printer Printer { get; private set; }

        public static bool InitUPOSPrinter(string printerName ,out string errorMessage)
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

            CurrentPrinterType = PrinterTypes.OPOS;
            return true;
        }

        public static bool InitESCPrinter(string printerName)
        {
            Printer = new Printer(printerName);
            CurrentPrinterType = PrinterTypes.ESC;
            return true;
        }

        public static bool InitPrinter(PrinterTypes printerType, string printerName, out string errorMsg)
        {
            CurrentPrinterType = PrinterTypes.None;
            switch (printerType)
            {
                case PrinterTypes.OPOS:
                    return InitUPOSPrinter(printerName, out errorMsg);
                case PrinterTypes.ESC:
                    errorMsg = "";
                    return InitESCPrinter(printerName);
                default:
                    break;
            }
            errorMsg = "Neplatný typ tiskárny";
            return false;
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
            if (CurrentPrinterType == PrinterTypes.OPOS)
            {
                int lineWidth = UPrinter.RecLineChars > 0 ? UPrinter.RecLineChars : 48;
                string separator = new string('-', lineWidth);

                string alignedSeparator = UAlignText(separator, alignment);

                UPrinter.PrintNormal(PrinterStation.Receipt, alignedSeparator + "\r\n");
                return;
            }
            Printer.Separator();
        }

        public static void PrintPricedItem(string text, string price)
        {
            if (CurrentPrinterType == PrinterTypes.OPOS)
            {
                UPrinter.PrintNormal(PrinterStation.Receipt, text);
                UPrinter.PrintNormal(PrinterStation.Receipt, UAlignText(price, Alignment.Right) + "\r\n");
                return;
            }
            Printer.AppendWithoutLf(text);
            Printer.AlignRight();
            Printer.Append(price);
        }

        public static void PrintNormalItem(string text)
        {
            switch (CurrentPrinterType)
            {
                case PrinterTypes.OPOS:
                    UPrinter.PrintNormal(PrinterStation.Receipt, text);
                    break;
                case PrinterTypes.ESC:
                    Printer.Append(text);
                    break;
            }
        }

        private static string UAlignText(string text, Alignment alignment)
        {
            int lineWidth = UPrinter.RecLineChars > 0 ? UPrinter.RecLineChars : 48;

            switch (alignment)
            {
                case Alignment.Left:
                    return text.PadRight(lineWidth);
                case Alignment.Center:
                    int padding = (lineWidth - text.Length) / 2;
                    return text.PadLeft(padding + text.Length).PadRight(lineWidth);
                case Alignment.Right:
                    return text.PadLeft(lineWidth);
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
            PrintSeparator();
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
        ESC,
        None
    }

    public enum Payments
    {
        Cash,
        Card
    }
}
