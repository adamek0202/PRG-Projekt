using Pokladna.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Pokladna
{
    internal static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Info: Načítání konfigurace... [OK]");
            Console.Write("Info: inicializace databáze... ");
            if (!File.Exists(Environment.CurrentDirectory + "\\pokladna.db"))
            {
                if (MessageBox.Show("Databáze nebyla nenalezena", "Chyba databáze", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    Console.WriteLine("[Chyba]");
                    Console.WriteLine("Chyba: Databáze nenalezena");
                    return;
                }
            }
            int receiptNumber = DatabaseFunctions.LoadReceiptNumber();
            if(receiptNumber != 0)
            {
                GlobalPosPrinter.receiptId = receiptNumber;
                Console.WriteLine("[OK]");
            }
            else
            {
                GlobalPosPrinter.receiptId = 1;
                Console.WriteLine("[Chyba]");
                Console.WriteLine("Chyba: Chybí číslo poslední účtenky. Číslování bude resetováno...");
            }

            Application.SetCompatibleTextRenderingDefault(false);
            Console.Write("Info: Inicializace tiskárny... ");
            string errorMessage = GlobalPosPrinter.InitPrinter("BP-T3");
            DatabaseFunctions.orderId = 1;
            if (errorMessage != string.Empty)
            {
                Console.WriteLine("[Chyba]");
                Console.WriteLine($"Chyba: {errorMessage}");
                MessageBox.Show($"UPOS: {errorMessage}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Console.WriteLine("[OK]"); 
            }
            Console.Write("Info: Inicializace čtečky karet... ");
            if (!NfcReader.Instance.Start())
            {
                Console.WriteLine("[Chyba]");
            }
            else
            {
                Console.WriteLine("[OK]");
            }
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Console.WriteLine("Info: Spouštění aplikace...");
            Application.Run(new StartForm());
            File.WriteAllText("receiptNumber", GlobalPosPrinter.receiptId.ToString());
        }
    }
}