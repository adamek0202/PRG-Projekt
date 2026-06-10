using Pokladna.Configuration;
using Pokladna.Database;
using Pokladna.Forms;
using Pokladna.Helpers;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokladna
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Logujeme vše od úrovně Debug výše
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: Path.Combine(Environment.CurrentDirectory, "logs", "pokladna-.txt"),
                    rollingInterval: RollingInterval.Day, // Každý den vytvoří nový soubor (např. pokladna-20260525.txt)
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Warning, // DO SOUBORU JEN WARNING A VYŠŠÍ
                    buffered: false
                )
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(Console.Error);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    Log.Fatal(ex, "Kritická chyba mimo UI vlákno! Aplikace bude ukončena.");
                }
                else
                {
                    Log.Fatal("Kritická chyba mimo UI vlákno bez objektu výjimky: {ExceptionObject}", e.ExceptionObject);
                }

                Log.CloseAndFlush(); // Klíčové – bez toho v logu na disku nic nebude!
            };

            // Zachytí chyby z hlavního UI vlákna WinForms
            Application.ThreadException += (sender, e) =>
            {
                Log.Fatal(e.Exception, "Kritická chyba v UI vlákně kasy!");
                Log.CloseAndFlush();
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Log.Error(e.Exception, "Neošetřená chyba v asynchronní úloze (Task).");
                // e.SetObserved(); // Pokud nechceš, aby kvůli tomu aplikace spadla (závisí na nastavení configu)
            };
            try
            {
                Application.SetCompatibleTextRenderingDefault(false);
                QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                Log.Information("=== START POKLADNÍHO SYSTÉMU ===");

                Log.Information("Načítání konfigurace...");
                if (!Configuration.ConfigManager.Initialize())
                {
                    Log.CloseAndFlush();
                    return;
                }

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
                Log.Information("Inicializace HW...");
                Log.Information("Inicializace pokladní tiskárny...");
                string printerError = GlobalPosPrinter.InitPrinter(ConfigManager.Values.Devices.Printer.PrinterName);
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
                Log.Information("Inicializace HW dokončena");

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