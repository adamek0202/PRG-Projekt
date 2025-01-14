using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public int SumPrice;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var group = new ListViewGroup();
            AddHeadItem("Zinger double", 130, group);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Group != null)
            {
                foreach (var item in listView1.SelectedItems[0].Group.Items)
                {
                    listView1.Items.Remove((ListViewItem)item);
                }
                //UpdateSumPrice(-Convert.ToInt32(listView1.SelectedItems[0].Group.Items[1].SubItems[1]));
            }
        }

        private void AddHeadItem(string name, int price, ListViewGroup group)
        {
            listView1.Items.Add(new ListViewItem(new string[] { name, price.ToString() + "Kč"}, group) { BackColor = Color.Orange});
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
            sumBox.Text = SumPrice.ToString() + "Kč";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var group = new ListViewGroup();
            AddHeadItem("Zinger double menu", 210, group);
            AddSubItem(new string[] { "Zinger double", "Velke hranolky", "Bezedny napoj" }, group);
        }
    }
}
