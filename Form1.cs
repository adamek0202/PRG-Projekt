using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Projekt
{
    public partial class MainForm : Form
    {
        public int SumPrice;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectPreviousItem()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int currentIndex = listView1.SelectedIndices[0];
                if (currentIndex > 0)
                {
                    listView1.Items[currentIndex].Selected = false;
                    listView1.Items[currentIndex - 1].Selected = true;
                    listView1.Items[currentIndex - 1].EnsureVisible();
                }
            }
            else if (listView1.Items.Count > 0)
            {
                listView1.Items[listView1.Items.Count - 1].Selected = true;
                listView1.Items[listView1.Items.Count - 1].EnsureVisible();
            }
        }

        private void SelectNextItem()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                int currentIndex = listView1.SelectedIndices[0];
                if (currentIndex < listView1.Items.Count - 1)
                {
                    listView1.Items[currentIndex].Selected = false;
                    listView1.Items[currentIndex + 1].Selected = true;
                    listView1.Items[currentIndex + 1].EnsureVisible();
                }
            }
            else if (listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].EnsureVisible();
            }
        }

        private void AddHeadItem(string name, int price, ListViewGroup group)
        {
            listView1.Items.Add(new ListViewItem(new string[] { name, price.ToString() + "Kč" , "1"}, group) { BackColor = Color.Orange });
            UpdateSumPrice(price);
        }

        private void AddSubItem(string[] names, ListViewGroup group)
        {
            foreach (var item in names)
            {
                listView1.Items.Add(new ListViewItem(item, group) { BackColor = Color.Yellow });
            }
        }

        private void UpdateSumPrice(int price)
        {
            SumPrice += price;
            sumLabel.Text = "Celkem " + SumPrice.ToString() + "Kč";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddHeadItem("Zinger double", 130, new ListViewGroup());
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Group != null)
            {
                if (MessageBox.Show("Opravdu chctete tuto položku stornovat?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (listView1.SelectedItems[0].Group.Items.Count > 0)
                    {
                        UpdateSumPrice(-Convert.ToInt32(new string(listView1.SelectedItems[0].Group.Items[0].SubItems[1].Text.Where(char.IsDigit).ToArray())));

                    }
                    foreach (var item in listView1.SelectedItems[0].Group.Items)
                    {
                        listView1.Items.Remove((ListViewItem)item);
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

        private void button4_Click(object sender, EventArgs e)
        {
            var group = new ListViewGroup();
            AddHeadItem("Zinger double menu", 210, group);
            AddSubItem(new string[] { "Zinger double", "Male hranolky", "Bezedny napoj" }, group);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (MessageBox.Show("Opravdu chcete tento účet stornovat?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    listView1.Items.Clear();
                    listView1.Groups.Clear();
                    UpdateSumPrice(-SumPrice);
                } 
            }
            else
            {
                MessageBox.Show("Není co stornovat", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1)
            {
                listView1.SelectedItems[0].BackColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddHeadItem("Zinger", 120, new ListViewGroup());
        }
    }
}
