using CsvHelper;
using Pokladna.Database;
using Pokladna.Exporters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    // Dědí z BaseForm
    public partial class SalesForm : BaseForm
    {
        private List<Sale> _sales = new();

        // Konstruktor přijímá sdílený kontext kasy
        internal SalesForm(PosContext context) : base(context)
        {
            InitializeComponent();

            // Pokud používáš custom stylování pro ListView:
            // NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);

            LoadItemsToUi();
        }

        private void LoadItemsToUi(string user = "")
        {
            listViewWithScrollBar1.Items.Clear();

            // Načtení transakcí z MySQL přes databázovou vrstvu
            _sales = DatabaseFunctions.LoadTransactions(user);

            foreach (var item in _sales)
            {
                var row = new ListRow(new string[]
                {
                    item.Number.ToString(),
                    item.DateAndTime.ToString("dd.MM.yyyy"),
                    item.DateAndTime.ToString("HH:mm"), // Oprava formátu (09:05 místo 9:5)
                    $"{item.Price} Kč",
                    item.Payment,
                    item.User
                });

                listViewWithScrollBar1.Items.Add(row);
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (_sales.Count == 0) return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csv.WriteRecords(_sales);
                    }
                    Serilog.Log.Information("Historie tržeb exportována do CSV: {Path}", saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Chyba při exportu tržeb do CSV.");
                    MessageBox.Show("Soubor se nepodařilo uložit. Zkontrolujte, zda není otevřen v jiném programu.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (_sales.Count == 0) return;

            using (var preview = new PrintPreviewForm(PDFGeneration.GenerateTransactionsPdf(_sales)))
            {
                preview.ShowDialog();
            }
        }

        private void dateStripButton_Click(object sender, EventArgs e)
        {
            // Tady máš přípravu na filtraci podle data
            using (var dateForm = new DateSelectForm())
            {
                if (dateForm.ShowDialog() == DialogResult.OK)
                {
                    // Tady pak vytáhneš vybrané datum z dateFormu a zavoláš např:
                    // DateTime vybraneDatum = dateForm.SelectedDate;
                    // Následně by to chtělo novou metodu v DB např. LoadTransactionsByDate(vybraneDatum)
                }
            }
        }

        private void ExcelExport()
        {
            // Zatím nedodělaný export do XLSX – pokud budeš chtít, 
            // můžeme na to pak nahodit třeba knihovnu ClosedXML, ta je na to skvělá.
            var sfd = new SaveFileDialog
            {
                Title = "Export do Excelu",
                Filter = "Excel sešit|*.xlsx",
                DefaultExt = "xlsx",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Tady bude logika zápisu do excelu
            }
        }

        private void uživatelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tady můžeš vyvolat dialog pro výběr uživatele (zaměstnance) 
            // a překlopit zobrazení přes: LoadItemsToUi(vybranyUzivatel);
        }
    }
}