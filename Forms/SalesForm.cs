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
            string querry = "SELECT Number, DateTime, Price, Payment, User FROM Transactions";
            using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sales.Add(new Sale(
                            int.Parse(reader["number"].ToString()),
                            DateTime.Parse(reader["DateTime"].ToString()),
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
                    listViewWithScrollBar1.Items.Add(new ListViewItem(new string[] { item.Number.ToString(), item.DateAndTime.Date.ToShortDateString(), $"{item.DateAndTime.Hour}:{item.DateAndTime.Minute}",item.Price.ToString() + " Kč", item.Payment, item.User }));
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
                    using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
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
                new PrintPreviewForm(PDFGeneration.GenerateTransactionsPdf(Sales)).ShowDialog();
            }
        }
    }

    internal class Sale
    {
        public Sale(int number, DateTime dateAndTime, int price, string payment, string user)
        {
            Number = number;
            DateAndTime = dateAndTime;
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
        public DateTime DateAndTime { get; set; }
        public int Price { get; set; }
        public string Payment { get; set; }
        public string User { get; set; }
    }
}
