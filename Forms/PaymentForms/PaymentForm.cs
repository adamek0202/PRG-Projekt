using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;


namespace Pokladna.Forms
{
    public partial class PaymentForm : Form
    {
        public static int Price { get; private set; }
        private static int Discount { get; set; } = 0;

        internal PaymentForm(int price, List<ListRow> data)
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
            int hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void LoadListViewData(List<ListRow> data)
        {
            foreach (var item in data)
            {
                listView1.Items.Add((ListRow)item.Clone());
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

        private void KRemoveButton_Click(object sender, EventArgs e)
        {
            if (PayedTextBox.Text.Length >= 1)
            {
                PayedTextBox.Text = PayedTextBox.Text.Remove(PayedTextBox.Text.Length - 1);
            }
        }

        private void CashButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(PayedTextBox.Text) >= Price)
                {
                    PostPayment(Payments.Cash);
                    new TenderedReturnForm(Convert.ToInt32(PayedTextBox.Text) - Price).ShowDialog();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception) {}
        }

        private void ExactCashButton_Click(object sender, EventArgs e)
        {
            PostPayment(Payments.Cash);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            new CardPaymentProcessForm().ShowDialog();
            PostPayment((string)btn.Tag == "FoodCard" ? Payments.FoodCard : Payments.Card);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PostPayment(Payments payment)
        {
            DatabaseFunctions.RecordSale(listView1, payment, Price);
            DatabaseFunctions.SendOrderName(listView1);
            //await DatabaseFunctions.ProcessListViewAndSend(listView1, "127.0.0.1");
            Receipt.PrintReceipt(listView1, payment, Price, Discount);
        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            var df = new DiscountForm();
            if(df.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Add(new ListRow(new string[] { "Sleva", $"-{Math.Round((double)df.Discount / 100 * Price).ToString()} Kč" }) { BackColor = Color.Lime});
                Price -= (int)Math.Round((double)df.Discount / 100 * Price);
                Discount = (int)Math.Round((double)df.Discount / 100 * Price);
                sumLabel.Text = $"Celkem: {Price} Kč";
            }
        }

        private void giftCardButton_Click(object sender, EventArgs e)
        {

        }
    }
}
