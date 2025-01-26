using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
                EPrinter.Append("IČO: 12345678");
                EPrinter.Append("DIČ: CZ12345678");
                EPrinter.AlignLeft();
                EPrinter.Append(FormatTwoColumns("Obsluha: Adam", "Pokladna: 1", 48));
                EPrinter.Separator();
                EPrinter.Append(FormatTwoColumns(receiptId.ToString("D5"), DateTime.UtcNow.ToString(), 48));
                EPrinter.AlignCenter();
                EPrinter.AlignLeft();
                EPrinter.Separator();
                EPrinter.AlignCenter();
                EPrinter.DoubleWidth2();
                EPrinter.Append(MainForm.Here ? "V Restauraci" : "S sebou");
                EPrinter.NormalWidth();
                EPrinter.NewLine();
                EPrinter.AlignLeft();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].SubItems.Count > 1)
                    {
                        EPrinter.Append(FormatTwoColumns($"{listView1.Items[i].SubItems[2].Text} {listView1.Items[i].Text}", listView1.Items[i].SubItems[1].Text, 48));
                    }
                    else
                    {
                        EPrinter.Append($"   -{listView1.Items[i].Text}");
                    }
                }
                PrintPayment(paymentType, Price, PayedTextBox.Text.Length > 0 ? int.Parse(PayedTextBox.Text): 0);
                EPrinter.NewLine();
                EPrinter.AlignCenter();
                EPrinter.Append("Děkujeme vám za váš nákup");
                EPrinter.Append("Na shledanou");
                EPrinter.AlignLeft();
                EPrinter.NewLines(2);
                EPrinter.PartialPaperCut();
                EPrinter.PrintDocument();
                EPrinter.Clear();
                receiptId++;
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

        private void CashButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(PayedTextBox.Text) >= Price)
            {
                RecordSale();
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
            RecordSale();
            PrintReceipt(Payments.Cash);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var cardProcess = new CardPaymentProcessForm();
            cardProcess.ShowDialog();
            RecordSale();
            PrintReceipt((string)btn.Tag == "FoodCard" ? Payments.FoodCard : Payments.Card);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RecordSale()
        {
            var connection = DatabaseConnection.Connection;
            string querry = "UPDATE Products SET Sold = sold + 1 WHERE Name = @name";

            foreach (ListViewItem item in listView1.Items)
            {
                // Zjisti počet podle pravidel
                int count = 0;

                if (item.SubItems.Count > 1) // Pokud má položka 2 subpoložky
                {
                    count = int.Parse(item.SubItems[2].Text); // Počet je v druhé subpoložce
                }
                else if (item.Group != null && item.Group.Items.Count > 0 && item.Group.Items[0].SubItems.Count > 1)
                {
                    // Pokud nemá subpoložky, vezmi počet z první položky skupiny
                    count = int.Parse(item.Group.Items[0].SubItems[2].Text);
                }

                // Proveď SQL příkaz pro každý počet
                for (int i = 0; i < count; i++)
                {
                    string productName = item.Text;

                    using (var command = new SQLiteCommand(querry, connection))
                    {
                        command.Parameters.AddWithValue("@name", productName);
                        Console.WriteLine(command.ExecuteNonQuery());
                    }
                }
            }
        }
    }
}
