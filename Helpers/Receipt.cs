using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using Pokladna.Configuration;
using Pokladna.Dto;
using Pokladna.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Pokladna
{
    internal static class Receipt
    {
        const int WIDTH = 48;

        private static byte[] L(string text) => Encoding.GetEncoding(1250).GetBytes(text);
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
            //if (EPrinter == null) return;

            try
            {
                var e = new EPSON();
                var receipt = new List<byte[]>();
                ref List<byte[]> receiptParts = ref receipt;

                receipt.Add(e.Initialize());
                receipt.Add(new byte[] { 0x1B, 0x1D, 0x74, 33 });
                receipt.Add(new byte[] { 0x1D, 0x4C, 0x00, 0x00 });
                receipt.Add(new byte[] { 0x1D, 0x57, 0x40, 0x02 });

                receipt.Add(e.CenterAlign());
                receipt.Add(L("Spoje Kolín\n"));
                receipt.Add(L("Jaselská 826, 280 12 Kolín\n"));

                receipt.Add(e.LeftAlign());
                receipt.Add(L(FormatTwoColumns($"Obsluha: {cashierName}", "Pokladna: 1", WIDTH) + "\n"));
                receipt.Add(GetLineSeparator('-', WIDTH));
                receipt.Add(L(FormatTwoColumns(receiptId.ToString("D5"), DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), WIDTH) + "\n"));
                receipt.Add(GetLineSeparator('-', WIDTH));

                receiptParts.Add(e.CenterAlign());
                receiptParts.Add(e.SetStyles(PrintStyle.DoubleWidth | PrintStyle.DoubleHeight));
                receiptParts.Add(L((isHere ? "S Sebou" : "V Restauraci") + "\n"));
                receiptParts.Add(e.SetStyles(PrintStyle.None));
                receiptParts.Add(e.FeedLines(1));

                receiptParts.Add(e.LeftAlign());
                byte[] itemsBytes = GetItemsBytes(currentCart, WIDTH);
                receiptParts.Add(itemsBytes);

                // Tisk patičky platby, DPH a slev
                receiptParts.Add(PrintPayment(paymentType, totalPrice, paid, e, discount));

                receiptParts.Add(e.CenterAlign());
                receiptParts.Add(L("Děkujeme za váš nákup.\n"));
                receiptParts.Add(L("Na shledanou\n"));
                receiptParts.Add(e.FeedLines(3));
                receiptParts.Add(e.PartialCut());

                byte[] payload = ByteSplicer.Combine(receipt.ToArray());
                Console.WriteLine(payload.Length);
                File.WriteAllBytes(@"E:\uctenka.bin", payload);

                RawPrinterHelper.SendBytesToPrinter(ConfigManager.Values.Devices.Printer.PrinterName, payload);

            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Kritická chyba hardwaru při tisku účtenky č. {ReceiptId}", receiptId);
            }
        }

        private static byte[] GetLineSeparator(char character, int length)
        {
            string line = new string(character, length) + "\n";
            return Encoding.GetEncoding(1250).GetBytes(line);
        }

        private static byte[] GetItemsBytes(List<SaleItemDto> items, int width)
        {
            var itemParts = new List<byte[]>();

            foreach (var item in items)
            {
                string leftColumn = $"{item.Count} {item.Name}";
                string rightColumn = $"{item.TotalPrice}.00";

                // Naformátujeme řádek a převedeme do win-1250
                string row = FormatTwoColumns(leftColumn, rightColumn, width) + "\n";
                itemParts.Add(L(row));

                // 1. Pokud je to Menu, vytiskneme komponenty A JEJICH POZNÁMKY
                if (item.IsMenu && item.ComponentNames != null)
                {
                    foreach (var component in item.ComponentNames)
                    {
                        // Vytiskneme samotnou komponentu (např.   -Hranolky)
                        itemParts.Add(L($"  -{component}\n"));

                        // PŘIDÁNO: Podíváme se, jestli tato konkrétní komponenta má nějaké poznámky
                        if (item.ComponentNotes != null && item.ComponentNotes.TryGetValue(component, out var compNotes))
                        {
                            foreach (var compNote in compNotes)
                            {
                                // Vytiskneme poznámku pod komponentu s větším odsazením (např.     *Bez soli)
                                itemParts.Add(L($"   *{compNote}\n"));
                            }
                        }
                    }
                }

                // 2. Pokud jsou u položky (např. u burgeru nebo celého menu) obecné poznámky
                if (item.Notes != null && item.Notes.Count > 0)
                {
                    foreach (var note in item.Notes)
                    {
                        itemParts.Add(L($" *{note}\n"));
                    }
                }
            }

            // Spojíme všechny položky do jednoho byte[]
            return ByteSplicer.Combine(itemParts.ToArray());
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

        public static byte[] PrintPayment(Payments payment, int totalPrice, int tendered, EPSON e, int discount = 0)
        {
            List<byte[]> bytes = new List<byte[]>();

            bytes.Add(GetLineSeparator('-', WIDTH));

            // Pokud byla uplatněna sleva, ukážeme mezisoučet před slevou a hodnotu slevy
            if (discount > 0)
            {
                bytes.Add(L(FormatTwoColumns("Mezisoučet", $"{totalPrice + discount} Kč", WIDTH) + "\n"));
                bytes.Add(L(FormatTwoColumns("Sleva", $"-{discount} Kč", WIDTH) + "\n"));
            }

            // Korektní legislativní výpočet DPH 12% zpětným způsobem:
            // Základ = Celková cena / 1.12
            double taxRate = 0.12;
            double basePrice = totalPrice / (1.0 + taxRate);
            double vatAmount = totalPrice - basePrice;

            // :F2 zajistí zaokrouhlení na přesně 2 desetinná místa (např. 100.00 Kč)
            bytes.Add(L(FormatTwoColumns("Základ", $"{basePrice:F2} Kč", WIDTH)));
            bytes.Add(L(FormatTwoColumns("DPH 12%", $"{vatAmount:F2} Kč", WIDTH)));

            bytes.Add(GetLineSeparator('-', WIDTH));
            bytes.Add(L(FormatTwoColumns("Celkem", $"{totalPrice} Kč", WIDTH) + "\n"));
            bytes.Add(GetLineSeparator('=', WIDTH));

            // Výčet platebních metod už bez vazby na statický formulář
            if (payment == Payments.Cash)
            {
                // Pojistka: Pokud obsluha nezadala přijatou hotovost, dosadíme přesnou cenu
                int safeTendered = tendered > 0 ? tendered : totalPrice;

                bytes.Add(L(FormatTwoColumns("Hotově", $"{safeTendered} Kč", WIDTH) + "\n"));
                bytes.Add(L(FormatTwoColumns("Vráceno", $"{safeTendered - totalPrice} Kč", WIDTH) + "\n"));
            }
            else if (payment == Payments.Card)
            {
                bytes.Add(L(FormatTwoColumns("Karta", $"{totalPrice} Kč", WIDTH) + "\n"));
            }
            else // Payments.FoodCard
            {
                bytes.Add(L(FormatTwoColumns("Stravenková karta", $"{totalPrice} Kč", WIDTH) + "\n"));
            }

            bytes.Add(GetLineSeparator('-', WIDTH));
            return ByteSplicer.Combine(bytes.ToArray());
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