using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;
using ESC_POS_USB_NET.Printer;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Projekt.Forms
{
    public partial class PaymentForm : Form
    {
        private int Price;

        private enum Payments
        {
            Cash,
            CashExact,
            Card
        }

        private PrintDocument printDocument;

        public PaymentForm(int price ,List<ListViewItem> data)
        {
            InitializeComponent();
            Price = price;
            LoadListViewData(data);
            ReallyCenterToScreen(this);
        }

        private void ConsolePrint(Payments paymentType)
        {
            Console.WriteLine("Vítejte");
            for (int i = 0; i < 32; i++) Console.Write("-");
            Console.WriteLine();
            int padding;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems.Count > 1)
                {
                    Console.Write(listView1.Items[i].Text);
                    padding = 32 - (listView1.Items[i].Text.Length + listView1.Items[i].SubItems[1].Text.Length);
                    for (int index = 0; index < padding; index++) Console.Write(" ");
                    Console.WriteLine(listView1.Items[i].SubItems[1].Text);
                }
                else
                {
                    Console.Write("-" + listView1.Items[i].Text);
                    Console.WriteLine();
                }
            }
            for (int i = 0; i < 32; i++) Console.Write("-");
            Console.WriteLine();
            padding = 23 - Price.ToString().Length;
            Console.Write("Celkem");
            for (int i = 0; i < padding; i++) Console.Write(" ");
            Console.WriteLine($"{Price} kč");
            switch (paymentType)
            {
                case Payments.Cash | Payments.CashExact:
                    padding = 20;
                    Console.Write("Hotově");
                    for (int i = 0; i < padding; i++) Console.Write(" ");
                    if (paymentType == Payments.Cash)
                    {
                        Console.WriteLine($"{PayedTextBox.Text} Kč");
                        padding = 20;
                        Console.Write("Vratit");
                        for (int i = 0; i < padding; i++) Console.Write(" ");
                        Console.WriteLine($"{int.Parse(PayedTextBox.Text) - Price} Kč"); 
                    }
                    else
                    {
                        Console.WriteLine($"{Price} Kč");
                    }
                    break;
            }
            for (int i = 0; i < 32; i++) Console.Write("-");
            Console.WriteLine();
        }

        private void PrintReceipt(Payments paymentType)
        {
            ConsolePrint(paymentType);
            Printer printer = new Printer("POS-58-Series");
            printer.AlignCenter();
            printer.Append("Pokladna");
            printer.AlignLeft();
            for (int i = 0; i < 32; i++) printer.AppendWithoutLf("-");
            printer.NewLine();
            int padding;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems.Count > 1)
                {
                    printer.AppendWithoutLf(listView1.Items[i].Text);
                    padding = 32 - (listView1.Items[i].Text.Length + listView1.Items[i].SubItems[1].Text.Length);
                    for (int index = 0; index < padding; index++) printer.AppendWithoutLf(" ");
                    printer.Append(listView1.Items[i].SubItems[1].Text);
                    printer.AlignLeft();
                }
                else
                {
                    printer.Append($"-{listView1.Items[i].Text}");
                }
            }
            for (int i = 0; i < 32; i++) printer.AppendWithoutLf("-");
            printer.NewLine();
            printer.AppendWithoutLf("Celkem");
            padding = 32 - 8 - Price.ToString().Length;
            for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
            printer.Append($"{Price}Kč");
            switch (paymentType)
            {
                case Payments.Cash:
                case Payments.CashExact:
                    printer.AppendWithoutLf("Hotově");
                    if(paymentType == Payments.CashExact)
                    {
                        padding = 32 - 8 - Price.ToString().Length;
                        for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
                        printer.Append($"{Price}Kč");
                    }
                    else
                    {
                        padding = 32 - 6 - (PayedTextBox.Text.Length + 2);
                        for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
                        printer.Append($"{PayedTextBox.Text}Kč"); 
                    }
                    printer.AppendWithoutLf("Vratit");
                    if (paymentType == Payments.Cash)
                    {
                        padding = 32 - 8 - PayedTextBox.Text.Length;
                        for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
                        printer.Append($"{int.Parse(PayedTextBox.Text) - Price}Kč");
                        break;
                    }
                    padding = 32 - 6 - 3;
                    for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
                    printer.Append("0Kč");
                    break;
                case Payments.Card:
                    padding = 32 - 7 - Price.ToString().Length;
                    printer.AppendWithoutLf("Karta");
                    for (int i = 0; i < padding; i++) printer.AppendWithoutLf(" ");
                    printer.Append($"{Price}Kč");
                    break;
            }
            for (int i = 0; i < 32; i++) printer.AppendWithoutLf("-");
            printer.NewLine();
            printer.AlignCenter();
            printer.Append("Dekujeme vam za vas nakup");
            printer.NewLines(4);
            printer.FullPaperCut();
            printer.PrintDocument();
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
            PrintReceipt(Payments.CashExact);
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
