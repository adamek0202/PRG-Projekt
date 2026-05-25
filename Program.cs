using System;
using System.IO;
using System.Windows.Forms;
using Serilog;
using Pokladna.Forms;
using Pokladna.Database;

namespace Pokladna
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 1. NASTAVENÍ SERILOGU
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Logujeme vše od úrovně Debug výše
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: Path.Combine(Environment.CurrentDirectory, "logs", "pokladna-.txt"),
                    rollingInterval: RollingInterval.Day, // Každý den vytvoří nový soubor (např. pokladna-20260525.txt)
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            // 2. GLOBÁLNÍ ODCHYTÁVÁNÍ CHYB
            // Zachytí chyby z ne-UI vláken
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                Log.Fatal(e.ExceptionObject as Exception, "Kritická chyba mimo UI vlákno!");

            // Zachytí chyby z hlavního UI vlákna WinForms
            Application.ThreadException += (sender, e) =>
                Log.Fatal(e.Exception, "Kritická chyba v UI vlákně kasy!");

            try
            {
                Application.SetCompatibleTextRenderingDefault(false);
                QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                Log.Information("=== START POKLADNÍHO SYSTÉMU ===");

                // --- DATABÁZE ---
                Log.Information("Připojování k MySQL databázi...");
                if (!DatabaseConnection.IsConnectionValid(out string dbError))
                {
                    Log.Error("Chyba připojení k databázi: {Error}", dbError);
                    MessageBox.Show($"Nepodařilo se připojit k databázovému serveru:\n{dbError}",
                        "Chyba databáze", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Log.Information("Kontrola integrity databázových tabulek...");
                if (!DatabaseValidator.CheckDatabaseIntegrity() || !DatabaseValidator.CheckTables())
                {
                    Log.Warning("Integrita databáze neodpovídá schématu. Pokus o automatickou nápravu/vytvoření tabulek...");

                    try
                    {
                        // Spustíme skript, který dotiskne chybějící tabulky (fastfood_db, Transactions, atd.)
                        DatabaseFunctions.InitializeDatabaseSchema();

                        Log.Information("Struktura DB byla úspěšně opravena. Spouštím kontrolu znovu...");

                        // Zkusíme validaci podruhé. Pokud i teď selže, je v kódu nebo právech vážnější chyba.
                        if (!DatabaseValidator.CheckDatabaseIntegrity() || !DatabaseValidator.CheckTables())
                        {
                            throw new Exception("Oprava proběhla, ale validace tabulek stále selhává.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "Integrita databáze byla porušena a automatická oprava selhala!");
                        MessageBox.Show("Chyba integrity databáze. Aplikaci nebylo možné automaticky opravit a nemůže bezpečně nastartovat.",
                            "Kritická chyba", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                Log.Information("Databáze je v pořádku. Pokračuji ve startu aplikace...");

                // --- STAVOVÁ DATA ---
                Log.Information("Načítání čísla účtenky...");
                int receiptNumber = DatabaseFunctions.LoadReceiptNumber();
                PosContext posContext = new PosContext
                {
                    NextReceiptId = receiptNumber > 0 ? receiptNumber : 1,
                    CurrentCashierName = "Adam" // Sem v budoucnu propadne jméno z přihlašovacího okna
                };

                GlobalPosPrinter.receiptId = receiptNumber;
                DatabaseFunctions.orderId = 1;

                // --- HARDWARE ---
                Log.Information("Inicializace pokladní tiskárny...");
                string printerError = GlobalPosPrinter.InitPrinter(posContext.PrinterName);
                if (!string.IsNullOrEmpty(printerError))
                {
                    Log.Warning("Tiskárna nebyla inicializována: {Error}", printerError);
                    MessageBox.Show($"Tiskárna nebyla plně inicializována: {printerError}",
                        "Varování hardwaru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Log.Information("Inicializace NFC čtečky karet...");
                if (!NfcReader.Instance.Start())
                {
                    Log.Warning("NFC čtečka nebyla nalezena. Přihlašování bude možné pouze kódem.");
                }

                Log.Information("Všechny subsystémy připraveny. Spouštění GUI...");
                Application.Run(new StartForm());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Aplikace neočekávaně havarovala při startu!");
            }
            finally
            {
                Log.Information("=== UKONČENÍ POKLADNÍHO SYSTÉMU ===");
                Log.CloseAndFlush(); // Důležité! Vyprázdní zápisové buffery na disk
            }
        }
    }
}