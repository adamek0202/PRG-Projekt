using CsvHelper;
using Pokladna.Database;
using Pokladna.Exporters;
using Pokladna.Forms.Controls;
using Pokladna.Forms.ProductSelectionForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Pokladna.Forms.ManagementForms
{
    // Dědí z BaseForm
    internal partial class ItemSalesForm : BaseForm
    {
        // Seznam načtených dat pro exporty
        private List<SoldProduct> _items = new();

        // Konstruktor přijímá kontext
        internal ItemSalesForm(PosContext context) : base(context)
        {
            InitializeComponent();

            // Pokud má tvůj custom ListView tuhle metodu pro starší skinování:
            // NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);

            LoadItemsToUi();
        }

        private void LoadItemsToUi()
        {
            listViewWithScrollBar1.Items.Clear();

            // Vytáhneme data z MySQL přes databázovou vrstvu
            _items = DatabaseFunctions.GetSoldProductsReport();

            // Naplníme grafické komponenty
            foreach (var item in _items)
            {
                var row = new ListRow(new string[] {
                    item.Id.ToString(),
                    item.Name,
                    $"{item.Price} Kč",
                    item.Count.ToString(),
                    $"{item.Count * item.Price} Kč"
                });

                listViewWithScrollBar1.Items.Add(row);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0) return;

            // Předpokládám, že PDF generation si upravíš na nový typ objektu SoldProduct, nebo použiješ generický list
            var pdfDocument = PDFGeneration.GenerateProductsPdf(_items);
            using (var preview = new PrintPreviewForm(pdfDocument))
            {
                preview.ShowDialog();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0) return;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csv.WriteRecords(_items);
                    }
                    Serilog.Log.Information("Statistika prodejů úspěšně exportována do CSV: {Path}", saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Chyba při exportu statistik do CSV.");
                    MessageBox.Show("Nepodařilo se uložit CSV soubor. Data jsou pravděpodobně blokována jiným programem.", "Chyba exportu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}