using Pokladna.Forms;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Pokladna
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.Write("Info: Načítání konfigurace... ");
            Console.WriteLine("[OK]");
            Console.Write("Info: Inicializace prostředí... ");
            Console.WriteLine("[OK]");
            Console.Write("Info: Inicializace databáze... ");
            if (!File.Exists(Environment.CurrentDirectory + "\\pokladna.db") && !(DatabaseConnection.IsConnectionValid(out string error)))
            {
                Console.WriteLine("[Chyba]");
                Console.WriteLine("Chyba: Databáze nenalezena");
                MessageBox.Show("Databáze nebyla nenalezena", "Chyba databáze", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Console.WriteLine("[OK]");
            }
            Console.Write("Info: Kontrola integrity databáze... ");
            if (!DatabaseFunctions.CheckDatabaseIntegrity())
            {
                Console.WriteLine("[Chyba]");
                Console.WriteLine("Chyba: Integrita databáze byla porušena");
                return;
            }
            else
            {
                Console.WriteLine("[OK]");
            }
            Console.Write("Info: Kontrola integrity databázových tabulek... ");
            if (DatabaseFunctions.CheckTables())
            {
                Console.WriteLine("[OK]");
            }
            else
            {
                Console.WriteLine("[Chyba]");
                return;
            }

            Console.Write("Info: Načítání stavových dat...");
            int receiptNumber = DatabaseFunctions.LoadReceiptNumber();
            if (receiptNumber != 0)
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
        }
    }
}