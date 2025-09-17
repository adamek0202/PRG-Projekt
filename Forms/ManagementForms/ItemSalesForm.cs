using CsvHelper;
using Pokladna;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class ItemSalesForm : Form
    {
        private List<Product> Items;

        public ItemSalesForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);
            LoadItems();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DWMNCRENDERINGPOLICY renderingPolicy = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            int hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LoadItems()
        {
            Items = new List<Product>();
            string querry = "SELECT ProductId, Name, Price, Sold FROM Products WHERE Sold > 0";
            using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Items.Add(new Product(
                            int.Parse(reader["ProductId"].ToString()),
                            reader["Name"].ToString(),
                            int.Parse(reader["Price"].ToString()),
                            int.Parse(reader["Sold"].ToString())
                        ));
                    }
                }
            }

            if (Items.Count > 0)
            {
                foreach (var item in Items)
                {
                    listViewWithScrollBar1.Items.Add(new ListViewItem(new string[] {
                        item.Id.ToString(),
                        item.Name,
                        item.Price.ToString() + " Kč",
                        item.Count.ToString(),
                        (item.Count * item.Price).ToString() + " Kč"
                    }));
                }
            }
        }

        internal class Product
        {
            public Product(int id, string name, int price, int count)
            {
                Id = id;
                Name = name;
                Price = price;
                Count = count;
            }

            public int Id { get; set;}
            public string Name { get; set; }
            public int Price { get; set; }
            public int Count { get; set; }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            new PrintPreviewForm(PDFGeneration.GenerateProductsPdf(Items)).ShowDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (Items.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csv.WriteRecords(Items);
                    }
                }
            }
        }
    }
}
