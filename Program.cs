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
            if (!File.Exists(Environment.CurrentDirectory + "\\pokladna.db"))
            {
                if (MessageBox.Show("Databáze nebyla nenalezena\nPřejete si vytvořit novou?", "Chyba databáze", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            if (File.Exists("receiptNumber"))
            {
                try
                {
                    GlobalPosPrinter.receiptId = int.Parse(File.ReadAllText("receiptNumber"));
                }
                catch (FormatException)
                {
                    MessageBox.Show("Data byla poškozena\nČíslování dokladů bude resetováno", "Chyba dat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("receiptNumber");
                    GlobalPosPrinter.receiptId = 1;
                }
            }
            else
            {
                MessageBox.Show("Chybí číslo posledního dokladu\nČíslování dokladů bude resetováno", "Chyba dat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GlobalPosPrinter.receiptId = 1;
            }
            Application.SetCompatibleTextRenderingDefault(false);
            string errorMessage = GlobalPosPrinter.InitPrinter("BP-T3");
            DatabaseFunctions.orderId = 1;
            if (errorMessage != string.Empty)
            {
                MessageBox.Show($"UPOS: {errorMessage}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Application.Run(new StartForm());
            File.WriteAllText("receiptNumber", GlobalPosPrinter.receiptId.ToString());
        }
    }
}
