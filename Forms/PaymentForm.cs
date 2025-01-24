using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;
using static Projekt.GlobalPosPrinter;


namespace Projekt.Forms
{
    public partial class PaymentForm : Form
    {
        private int Price;

        public PaymentForm(int price, List<ListViewItem> data)
        {
            InitializeComponent();
            Price = price;
            LoadListViewData(data);
            ReallyCenterToScreen(this);
        }

        private void PrintReceipt(Payments paymentType)
        {
            if (EPrinter != null)
            {
                EPrinter.AlignCenter();
                EPrinter.Append("Spoje Kolín");
                EPrinter.Append("Jaselská 826, 280 12 Kolín");
                EPrinter.Append("Provozovna: Spoje Kolín");
                EPrinter.AlignLeft();
                EPrinter.Append("Obsluha: Adam");
                EPrinter.Separator();
                EPrinter.Append(FormatTwoColumns("Uct: 00001", DateTime.UtcNow.ToString(), 48));
                EPrinter.AlignCenter();
                EPrinter.AlignLeft();
                EPrinter.Separator();
                EPrinter.AlignCenter();
                EPrinter.DoubleWidth2();
                EPrinter.Append("V restauraci");
                EPrinter.NormalWidth();
                EPrinter.NewLine();
                EPrinter.AlignLeft();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].SubItems.Count > 1)
                    {
                        EPrinter.Append(FormatTwoColumns(listView1.Items[i].Text, listView1.Items[i].SubItems[1].Text, 48));
                    }
                    else {
                        EPrinter.Append($"-{listView1.Items[i].Text}");
                    }
                }
                PrintPayment(paymentType, Price, PayedTextBox.Text.Length > 0 ? int.Parse(PayedTextBox.Text) - Price : 0);
                EPrinter.NewLine();
                EPrinter.AlignCenter();
                EPrinter.Append("Děkujeme vám za váš nákup");
                EPrinter.AlignLeft();
                EPrinter.NewLines(2);
                EPrinter.PartialPaperCut();
                EPrinter.PrintDocument();
                EPrinter.Clear();
            }
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
            if (Convert.ToInt32(PayedTextBox.Text) >= Price)
            {
                PrintReceipt(Payments.Cash);
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
            PrintReceipt(Payments.Cash);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var cardProcess = new CardPaymentProcessForm();
            cardProcess.ShowDialog();
            PrintReceipt(Payments.Card);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
