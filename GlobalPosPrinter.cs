using ESC_POS_USB_NET.Printer;
using System;
using System.Drawing.Printing;
using System.Management;

namespace Pokladna
{
    internal static class GlobalPosPrinter
    {
        public static Printer EPrinter;

        public static int receiptId;

        public static string InitPrinter(string printerName)
        {
            if (!PrinterExists(printerName))
            {
                EPrinter = null;
                return $"Chyba při inicializaci tiskárny: Tiskárna neexistuje";
            }
            if (!CheckPrinterStatus(printerName))
            {
                EPrinter = null;
                return $"Chyba při inicializaci tiskárny: Tiskárna je offline";
            }
            try
            {
                EPrinter = new Printer(printerName);
                return "";
            }
            catch (Exception ex)
            {
                EPrinter = null;
                return $"Chyba při inicializaci tiskárny: {ex.Message}";
            }
        }

        public static bool CheckPrinterStatus(string printerName)
        {
            string query = $"SELECT Name, PrinterStatus, WorkOffline FROM Win32_Printer WHERE Name = '{printerName.Replace("\\", "\\\\")}'";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject printer in searcher.Get())
                {
                    int printerStatus = Convert.ToInt32(printer["PrinterStatus"]);
                    bool workOffline = Convert.ToBoolean(printer["WorkOffline"]);

                    //Console.WriteLine($"Info: Tiskárna: {printer["Name"]}");
                    //Console.WriteLine($"Info: Status tiskárny: {printerStatus}");
                    //Console.WriteLine($"Info: Tiskárna Offline: {workOffline}");

                    if (workOffline || printerStatus == 7) // 7 = Printer Offline
                    {
                        //Console.WriteLine("Chyba: Tiskárna je offline.");
                        return false;
                    }
                    else
                    {
                        //Console.WriteLine("Tiskárna je online.");
                        return true;
                    }
                }
                return false;
            }
        }

        public static bool PrinterExists(string name)
        {
            foreach(string printerName in PrinterSettings.InstalledPrinters)
            {
                if(printerName == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
