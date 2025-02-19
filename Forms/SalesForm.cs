using CsvHelper;
using CsvHelper.Configuration;
using Pokladna;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Projekt.Forms
{
    public partial class SalesForm : Form
    {
        private List<Sale> Sales;

        public SalesForm()
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

        private void LoadItems()
        {
            Sales = new List<Sale>();
            string querry = "SELECT Date, Price, Payment, User FROM Transactions";
            using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sales.Add(new Sale(
                            2,
                            reader["Date"].ToString(),
                            int.Parse(reader["Price"].ToString()),
                            reader["Payment"].ToString(),
                            reader["User"].ToString()
                        ));
                    }
                }
            }

            if (Sales.Count > 0)
            {
                foreach (var item in Sales)
                {
                    listViewWithScrollBar1.Items.Add(new ListViewItem(new string[] { item.Number.ToString() ,item.Date, item.Price.ToString() + " Kč", item.Payment, item.User }));
                } 
            }
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            if (Sales.Count > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(Sales);
                    }
                } 
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if(Sales.Count > 0)
            {
                new PrintPreviewForm(Sales).ShowDialog();
            }
        }
    }

    internal class Sale
    {
        public Sale(int number, string date, int price, string payment, string user)
        {
            Number = number;
            Date = date;
            Price = price;
            switch (payment)
            {
                case "Cash":
                    Payment = "Hotovost";
                    break;
                case "Card":
                    Payment = "Platební karta";
                    break;
                case "FoodCard":
                    Payment = "Stravenková karta";
                    break;
                default:
                    break;
            }
            User = user;
        }

        public int Number { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }
        public string Payment { get; set; }
        public string User { get; set; }
    }
}
