using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projekt.Forms
{
    public partial class PaymentForm : Form
    {
        private int Price;

        private PrintDocument printDocument;

        public PaymentForm(int price ,List<ListViewItem> data)
        {
            InitializeComponent();
            Price = price;
            LoadListViewData(data);
            ReallyCenterToScreen(this);
        }

        private void PrintReceipt()
        {
            var printer = new UsbEscPosPrinter("lpt1");
            printer.PrintText("Pokladna");
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

        private void cashButton_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(PayedTextBox.Text) >= Price)
            {
                PrintReceipt();
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var cardProcess = new CardPaymentProcessForm();
            cardProcess.ShowDialog();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
