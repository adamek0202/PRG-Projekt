using ESC_POS_USB_NET.Printer;
using System;
using System.Management;

namespace Pokladna
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

        public static void CheckPrinterStatus(string printerName)
        {
            string query = $"SELECT Name, PrinterStatus, WorkOffline FROM Win32_Printer WHERE Name = '{printerName.Replace("\\", "\\\\")}'";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject printer in searcher.Get())
                {
                    int printerStatus = Convert.ToInt32(printer["PrinterStatus"]);
                    bool workOffline = Convert.ToBoolean(printer["WorkOffline"]);

                    Console.WriteLine($"Tiskárna: {printer["Name"]}");
                    Console.WriteLine($"Status: {printerStatus}");
                    Console.WriteLine($"Offline: {workOffline}");

                    if (workOffline || printerStatus == 7) // 7 = Printer Offline
                    {
                        Console.WriteLine("Tiskárna je offline.");
                    }
                    else
                    {
                        Console.WriteLine("Tiskárna je online.");
                    }
                }
            }
        }
    }
}
