using Pokladna.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using static Pokladna.GlobalPosPrinter;

namespace Pokladna
{
    internal static class Receipt
    {
        public static void PrintReceipt(
            List<SaleItemDto> currentCart,
            Payments paymentType,
            int totalPrice,
            int paid,
            int discount,
            int receiptId,
            string cashierName,
            bool isHere)
        {
            if (EPrinter == null) return;

            try
            {
                // 1. Tisk grafického loga (přes GDI+)
                using (PrintDocument pd = new PrintDocument())
                {
                    pd.PrinterSettings.PrinterName = "BP-T3";
                    pd.PrintPage += (sender, args) =>
                    {
                        Image img = LoadImageFromResources("Pokladna.Resources.logo_do_tiskarny.png");
                        if (img == null) return;

                        Rectangle marginBorders = args.PageBounds;
                        float scale = Math.Min((float)marginBorders.Width / img.Width, (float)marginBorders.Height / img.Height);
                        int width = (int)(img.Width * scale / 2);
                        int height = (int)(img.Height * scale / 2);
                        int offsetX = args.MarginBounds.X + (args.MarginBounds.Width - width) / 2;

                        args.Graphics.DrawImage(img, offsetX, marginBorders.Y, width, height);
                    };
                    pd.Print();
                }

                // 2. ESC/POS Hlavička
                EPrinter.AlignCenter();
                EPrinter.SetLineHeight(50);
                EPrinter.Append("Spoje Kolín");
                EPrinter.Append("Jaselská 826, 280 12 Kolín");
                EPrinter.NewLine();

                EPrinter.AlignLeft();
                EPrinter.Append(FormatTwoColumns($"Obsluha: {cashierName}", "Pokladna: 1", 48));
                EPrinter.Separator();
                EPrinter.Append(FormatTwoColumns(receiptId.ToString("D5"), DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), 48));
                EPrinter.Separator();

                EPrinter.AlignCenter();
                EPrinter.DoubleWidth2();
                EPrinter.Append(isHere ? "V Restauraci" : "S sebou");
                EPrinter.NormalWidth();
                EPrinter.NewLine();

                EPrinter.AlignLeft();

                // Procházení položek typu SaleItemDto
                foreach (var item in currentCart)
                {
                    // Mapování na vlastnosti DTO: Name, Count a TotalPrice
                    string leftColumn = $"{item.Count}x {item.Name}";
                    string rightColumn = $"{item.TotalPrice} Kč";

                    EPrinter.Append(FormatTwoColumns(leftColumn, rightColumn, 48));

                    // Pokud je to Menu, vytiskneme jeho komponenty (např. "Hranolky", "Kola")
                    if (item.IsMenu && item.ComponentNames != null)
                    {
                        foreach (var component in item.ComponentNames)
                        {
                            EPrinter.Append($"   -{component}");
                        }
                    }

                    // Pokud u položky visí nějaké kuchařské nebo jiné poznámky, vytiskneme je taky
                    if (item.Notes != null && item.Notes.Count > 0)
                    {
                        foreach (var note in item.Notes)
                        {
                            EPrinter.Append($"    * {note}");
                        }
                    }
                }

                // Tisk patičky platby, DPH a slev
                PrintPayment(paymentType, totalPrice, paid, discount);

                EPrinter.NewLine();
                EPrinter.AlignCenter();
                EPrinter.Append("Děkujeme za váš nákup.");
                EPrinter.Append("Na shledanou");
                EPrinter.NewLines(2);
                EPrinter.PartialPaperCut();

                EPrinter.PrintDocument();
                EPrinter.Clear();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Kritická chyba hardwaru při tisku účtenky č. {ReceiptId}", receiptId);
            }
        }

        private static string FormatTwoColumns(string leftText, string rightText, int totalWidth)
        {
            // Ochrana proti null hodnotám
            leftText ??= string.Empty;
            rightText ??= string.Empty;

            // Pokud je pravý text delší než samotná šířka řádku, ořízneme ho
            if (rightText.Length > totalWidth)
            {
                rightText = rightText.Substring(0, totalWidth);
            }

            // Oříznutí levého textu, pokud se spolu s pravým nevejdou na řádek
            if (leftText.Length + rightText.Length > totalWidth)
            {
                // Math.Max zajistí, že délka nikdy neklesne pod 0 (obrana proti zápornému Substringu)
                int allowedLeftLength = Math.Max(0, totalWidth - rightText.Length);
                leftText = leftText.Substring(0, allowedLeftLength);
            }

            // Výpočet mezer (zde už máme jistotu, že spaces nebude záporné)
            int spaces = totalWidth - leftText.Length - rightText.Length;

            // Sestavení řádku přes string interpolaci
            return $"{leftText}{new string(' ', spaces)}{rightText}";
        }

        public static void PrintPayment(Payments payment, int totalPrice, int tendered, int discount = 0)
        {
            if (EPrinter == null) return;

            EPrinter.Separator();

            // Pokud byla uplatněna sleva, ukážeme mezisoučet před slevou a hodnotu slevy
            if (discount > 0)
            {
                EPrinter.Append(FormatTwoColumns("Mezisoučet", $"{totalPrice + discount} Kč", 48));
                EPrinter.Append(FormatTwoColumns("Sleva", $"-{discount} Kč", 48));
            }

            // Korektní legislativní výpočet DPH 12% zpětným způsobem:
            // Základ = Celková cena / 1.12
            double taxRate = 0.12;
            double basePrice = totalPrice / (1.0 + taxRate);
            double vatAmount = totalPrice - basePrice;

            // :F2 zajistí zaokrouhlení na přesně 2 desetinná místa (např. 100.00 Kč)
            EPrinter.Append(FormatTwoColumns("Základ", $"{basePrice:F2} Kč", 48));
            EPrinter.Append(FormatTwoColumns("DPH 12%", $"{vatAmount:F2} Kč", 48));

            EPrinter.Separator();
            EPrinter.Append(FormatTwoColumns("Celkem", $"{totalPrice} Kč", 48));
            EPrinter.Separator('=');

            // Výčet platebních metod už bez vazby na statický formulář
            if (payment == Payments.Cash)
            {
                // Pojistka: Pokud obsluha nezadala přijatou hotovost, dosadíme přesnou cenu
                int safeTendered = tendered > 0 ? tendered : totalPrice;

                EPrinter.Append(FormatTwoColumns("Hotově", $"{safeTendered} Kč", 48));
                EPrinter.Append(FormatTwoColumns("Vráceno", $"{safeTendered - totalPrice} Kč", 48));
            }
            else if (payment == Payments.Card)
            {
                EPrinter.Append(FormatTwoColumns("Karta", $"{totalPrice} Kč", 48));
            }
            else // Payments.FoodCard
            {
                EPrinter.Append(FormatTwoColumns("Stravenková karta", $"{totalPrice} Kč", 48));
            }

            EPrinter.Separator();
        }

        /// <summary>
        /// Bezpečně načte obrázek z vestavěných prostředků sestavení.
        /// POZOR: Volající kód MUSÍ nad vráceným Image zavolat Dispose() nebo ho zabalit do using!
        /// </summary>
        private static Image LoadImageFromResources(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Stream se uvolní automaticky po opuštění bloku
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    // Žádný MessageBox! Zapíšeme to do logu a jedeme dál.
                    Serilog.Log.Error("Embedded resource nebyl nalezen: {ResourceName}", resourceName);
                    return null;
                }

                try
                {
                    return Image.FromStream(stream);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "Chyba při konverzi streamu na Image pro resource: {ResourceName}", resourceName);
                    return null;
                }
            }
        }
    }
}