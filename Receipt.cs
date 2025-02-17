using Pokladna.Forms;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static Pokladna.GlobalPosPrinter;

namespace Pokladna
{
    public enum Payments
    {
        Cash,
        Card,
        FoodCard
    }

    internal static class Receipt
    {
        private static int receiptId;
        public static void PrintReceipt(ListView listView, Payments paymentType, int paid, int discount = 0)
        {
            if (EPrinter != null)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = "BP-T3";
                pd.PrintPage += (sender, args) =>
                {
                    Image img = LoadImageFromResources("Projekt.Resources.logo_do_tiskarny.png");
                    Rectangle marginBorders = args.PageBounds;

                    float scale = Math.Min((float)marginBorders.Width / img.Width, (float)marginBorders.Height / img.Height);
                    int width = (int)(img.Width * scale / 2);
                    int height = (int)(img.Height * scale / 2);
                    int offsetX = args.MarginBounds.X + (args.MarginBounds.Width - width) / 2;

                    args.Graphics.DrawImage(img, offsetX, marginBorders.Y, width, height);
                };

                pd.Print();
                EPrinter.AlignCenter();
                EPrinter.SetLineHeight(50);
                EPrinter.Append("Spoje Kolín");
                EPrinter.Append("Jaselská 826, 280 12 Kolín");
                EPrinter.Append("Provozovna: Spoje Kolín");
                EPrinter.Append("IČO: 85142396");
                EPrinter.Append("DIČ: CZ74128942");
                EPrinter.NewLine();
                EPrinter.AlignLeft();
                EPrinter.Append(FormatTwoColumns($"Obsluha: {MainForm.Cashier}", "Pokladna: 1", 48));
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
                foreach (ListViewItem item in listView.Items)
                {
                    if (item.Text != "Sleva")
                    {
                        if (item.SubItems.Count > 1)
                        {
                            EPrinter.Append(FormatTwoColumns($"{item.SubItems[2].Text} {item.Text}", item.SubItems[1].Text, 48));
                        }
                        else
                        {
                            EPrinter.Append($"   -{item.Text}");
                        } 
                    }
                }
                PrintPayment(paymentType, paid.ToString().Length > 0 ? paid : 0, discount);
                EPrinter.NewLine();
                EPrinter.AlignCenter();
                EPrinter.Append("Děkujeme za vas nákup.");
                EPrinter.Append("Na shledanou");
                EPrinter.AlignLeft();
                EPrinter.NewLines(2);
                EPrinter.PartialPaperCut();
                EPrinter.PrintDocument();
                EPrinter.Clear();
                receiptId++;
            }
        }

        private static string FormatTwoColumns(string leftText, string rightText, int totalWidth)
        {
            // Oříznutí textu, pokud je příliš dlouhý
            if (leftText.Length + rightText.Length > totalWidth)
            {
                leftText = leftText.Substring(0, totalWidth - rightText.Length - 1);
            }

            // Počet mezer mezi texty
            int spaces = totalWidth - leftText.Length - rightText.Length;
            if (spaces < 0) spaces = 0;

            // Sestavení řádku
            return leftText + new string(' ', spaces) + rightText;
        }

        public static void PrintPayment(Payments payment, int tendered, int discount = 0)
        {
            EPrinter.Separator();
            if(discount > 0)
            {
                EPrinter.Append(FormatTwoColumns("Mezisoučet", $"{(PaymentForm.Price + discount).ToString()} Kč", 48));
                EPrinter.Append(FormatTwoColumns("Sleva", $"-{discount} Kč", 48));
            }
            EPrinter.Append(FormatTwoColumns("Základ", $" {PaymentForm.Price - ((double)12 / 100 * PaymentForm.Price)} Kč", 48));
            EPrinter.Append(FormatTwoColumns("DPH 12%", $"{(double)12 / 100 * PaymentForm.Price} Kč", 48));
            EPrinter.Separator();
            EPrinter.Append(FormatTwoColumns("Celkem", $"{PaymentForm.Price} Kč", 48));
            EPrinter.Separator('=');
            if (payment == Payments.Cash)
            {
                EPrinter.Append(FormatTwoColumns("Hotově", $"{tendered} Kč", 48));
                EPrinter.Append(FormatTwoColumns("Vráceno", $"{tendered - PaymentForm.Price} Kč", 48));
            }
            else if (payment == Payments.Card)
            {
                EPrinter.Append(FormatTwoColumns("Karta", $"{PaymentForm.Price} Kč", 48));
            }
            else
            {
                EPrinter.Append(FormatTwoColumns("Stravenková karta", $"{PaymentForm.Price} Kč", 48));
            }
            EPrinter.Separator();
        }

        private static Image LoadImageFromResources(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    MessageBox.Show("Resource nebyl nalezen: " + resourceName);
                    return null;
                }
                return Image.FromStream(stream);
            }
        }
    }
}
