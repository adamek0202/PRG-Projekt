using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;


namespace Projekt.Forms
{
    public partial class PaymentForm : Form
    {
        public static int Price { get; private set; }

        public PaymentForm(int price, List<ListViewItem> data)
        {
            InitializeComponent();
            Price = price;
            LoadListViewData(data);
            ReallyCenterToScreen(this);
        }

        

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DWMNCRENDERINGPOLICY renderingPolicy = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            int hr;
            hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void LoadListViewData(List<ListViewItem> data)
        {
            foreach (var item in data)
            {
                // Přidání položky včetně jejího formátování
                listView1.Items.Add((ListViewItem)item.Clone());
            }
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            NativeFunctions.DisableVisualStyles(listView1);
            sumLabel.Text = $"Celkem: {Price} Kč";
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void KeypadButton_Click(object sender, EventArgs e)
        {
            if (PayedTextBox.Text.Length < 5)
            {
                var btn = sender as System.Windows.Forms.Button;
                PayedTextBox.Text += btn.Tag;
            }
        }

        private void GiftCardButton_Click(object sender, EventArgs e)
        {

        }

        private void CashButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PayedTextBox.Text) >= Price)
            {
                DatabaseFunctions.RecordSale(listView1, Payments.Cash, Price);
                DatabaseFunctions.SendOrderName(listView1, new int[4] { 1, 2, 3, 4 });
                Receipt.PrintReceipt(listView1, Payments.Cash, int.Parse(PayedTextBox.Text));
                var returnBox = new TenderedReturnForm(Convert.ToInt32(PayedTextBox.Text) - Price);
                returnBox.ShowDialog();
                DialogResult = DialogResult.OK;
            }
        }

        private void KRemoveButton_Click(object sender, EventArgs e)
        {
            if (PayedTextBox.Text.Length >= 1)
            {
                PayedTextBox.Text = PayedTextBox.Text.Remove(PayedTextBox.Text.Length - 1);
            }
        }

        private void ExactCashButton_Click(object sender, EventArgs e)
        {
            DatabaseFunctions.RecordSale(listView1, Payments.Cash, Price);
            DatabaseFunctions.SendOrderName(listView1, new int[4] { 1, 2, 3, 4 });
            Receipt.PrintReceipt(listView1, Payments.Cash, Price);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var cardProcess = new CardPaymentProcessForm();
            cardProcess.ShowDialog();
            DatabaseFunctions.RecordSale(listView1 ,Payments.Card, Price);
            DatabaseFunctions.SendOrderName(listView1, new int[4] { 1, 2, 3, 4});
            Receipt.PrintReceipt(listView1, (string)btn.Tag == "FoodCard" ? Payments.FoodCard : Payments.Card, Price);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
