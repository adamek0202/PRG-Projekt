using Pokladna.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pokladna
{
    public partial class MainForm : Form
    {
        internal int SumPrice;
        private int Multiplier = 1;
        internal static int ExternalProduct { get; set; }

        internal static bool PriceCheck { get; set; } = false;

        internal bool replace = false;
        internal static bool Here { get; private set; } = true;
        internal static string Cashier { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Height = 1080;
		}

        public List<ListViewItem> ListViewData
        {
            get
            {
                List<ListViewItem> data = new List<ListViewItem>();
                foreach (ListViewItem item in listView.Items)
                {
                    data.Add((ListViewItem)item.Clone());
                }
                return data;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseConnection.CloseConnection();
        }

        #region Pohyb
        private void SelectPreviousItem()
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int currentIndex = listView.SelectedIndices[0];
                if (currentIndex > 0)
                {
                    listView.Items[currentIndex].Selected = false;
                    listView.Items[currentIndex - 1].Selected = true;
                    listView.Items[currentIndex - 1].EnsureVisible();
                }
            }
            else if (listView.Items.Count > 0)
            {
                listView.Items[listView.Items.Count - 1].Selected = true;
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }
        }

        private void SelectNextItem()
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int currentIndex = listView.SelectedIndices[0];
                if (currentIndex < listView.Items.Count - 1)
                {
                    listView.Items[currentIndex].Selected = false;
                    listView.Items[currentIndex + 1].Selected = true;
                    listView.Items[currentIndex + 1].EnsureVisible();
                }
            }
            else if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].EnsureVisible();
            }
        }
        #endregion Posun

        public void AddHeadItem(string name, int price, int times, ListViewGroup group, bool root = false)
        {
            listView.Items.Add(new ListViewItem(new string[] { name, price.ToString() + " Kč", times.ToString() }, group) { BackColor = Color.Orange, Tag = (root ? "root" : "") });
            UpdateSumPrice(price);
        }

        public void AddSubItem(string[] names, ListViewGroup group)
        {
            foreach (var item in names)
            {
                listView.Items.Add(new ListViewItem(item, group) { BackColor = Color.Yellow });
            }
        }

        private void UpdateSumPrice(int price)
        {
            SumPrice += price;
            sumLabel.Text = "Celkem " + SumPrice.ToString() + "Kč";
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Group != null)
            {
                if (new ConfirmForm("tuto položku stornovat").ShowDialog() == DialogResult.OK)
                {
                    if (listView.SelectedItems[0].Group.Items.Count > 0)
                    {
                        UpdateSumPrice(-Convert.ToInt32(new string(listView.SelectedItems[0].Group.Items[0].SubItems[1].Text.Where(char.IsDigit).ToArray())));

                    }
                    foreach (var item in listView.SelectedItems[0].Group.Items)
                    {
                        listView.Items.Remove((ListViewItem)item);
                    }
                }
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            SelectNextItem();
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            SelectPreviousItem();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                if (new ConfirmForm("tento účet stornovat").ShowDialog() == DialogResult.OK)
                {
                    listView.Items.Clear();
                    listView.Groups.Clear();
                    UpdateSumPrice(-SumPrice);
                }
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                replace = true;
            }
        }

        private void ItemButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            DatabaseFunctions.HandleButtonPress(this, Convert.ToInt32(btn.Tag), Multiplier);
            Multiplier = 1;
            multiplierLabel.Text = $"×{Multiplier}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NativeFunctions.DisableVisualStyles(listView);
            TopLevel = true;
            label1.Text += Cashier;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(new ConfirmForm("skončit").ShowDialog() == DialogResult.OK)
            {
                Close();
            }
        }

        private void PaymentButton_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count >= 1 && SumPrice != 0)
            {
                if (new PaymentForm(SumPrice, ListViewData).ShowDialog() == DialogResult.OK)
                {
                    listView.Items.Clear();
                    listView.Groups.Clear();
                    UpdateSumPrice(-SumPrice);
                }
            }
        }

        private void MultiplierButtons_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if ((string)btn.Tag == "X")
            {
                Multiplier = 0;
                multiplierLabel.Text = $"×{Multiplier}";
                return;
            }

            else if (Multiplier < 98)
            {
                try
                {
                    Multiplier = int.Parse(Multiplier.ToString() + btn.Tag);
                    multiplierLabel.Text = $"×{Multiplier}";
                    if (Multiplier > 99)
                    {
                        Multiplier = 0;
                        multiplierLabel.Text = $"×{Multiplier}";
                    }
                }
                catch
                {}
            }
        }

        private void BagButton_Click(object sender, EventArgs e)
        {
            AddHeadItem("Taska", 5, 1, new ListViewGroup(), true);
        }

        private void ExternalFormsButtons_Click(object sender, EventArgs e)
        {
            Form selectedForm = null;
            var btn = sender as Button;
            switch (btn.Tag)
            {
                case "BSmarts":
                    selectedForm = new BSmartsForm();
                    break;
                case "Drinks":
                    selectedForm = new DrinksForm();
                    break;
                case "Chicken":
                    selectedForm = new ChickenForm();
                    break;
                case "Supplements":
                    selectedForm = new SupplementsForm();
                    break;
                case "Desserts":
                    selectedForm = new DessertsForm();
                    break;
                case "Others":
                    selectedForm = new OthersForm();
                    break;
                default:
                    return;
            }
            if (selectedForm.ShowDialog() == DialogResult.OK)
            {
                DatabaseFunctions.HandleButtonPress(this, ExternalProduct, Multiplier);
                Multiplier = 1;
                multiplierLabel.Text = $"×{Multiplier}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (Here == true)
            {
                Here = false;
                btn.Text = "Tady";
            }
            else
            {
                Here = true;
                btn.Text = "S sebou";
            }
            LocationLabel.Text = Here ? "Tady" : "S sebou";
        }

        private void ManagerButton_Click(object sender, EventArgs e)
        {
            if (new LoginForm("manager").ShowDialog() == DialogResult.OK)
            {
                new ManagerForm().ShowDialog();
            }
        }

        private void priceAskButton_Click(object sender, EventArgs e) { PriceCheck = true; }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listView.SelectedItems.Count == 1 && !(new string[] { "root", "note" }.Contains(listView.SelectedItems[0].Tag)))
            {
                var noteForm = new ItemNotesForm();
                if(noteForm.ShowDialog() == DialogResult.OK)
                {
                    listView.Items.Insert(listView.SelectedItems[0].Index + 1, new ListViewItem(noteForm.Note, listView.SelectedItems[0].Group) { BackColor = Color.SpringGreen, Tag = "note"});
                }
            }
            else if (listView.SelectedItems.Count == 1 && (string)listView.SelectedItems[0].Tag == "note")
            {
                if(new ConfirmForm("tuto poznámku odstranit").ShowDialog() == DialogResult.OK)
                {
                    listView.Items.RemoveAt(listView.SelectedItems[0].Index);
                }
            }
        } 

        private void CouponsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
