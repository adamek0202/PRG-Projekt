using Pokladna.Exceptions;
using Pokladna.Forms;
using Pokladna.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    internal static class ButtonHandler
    {
        public static void HandleButtonPress(Order order, int buttonId, int times)
        {
            if (times < 1) return;

            // Tyto proměnné potřebujeme mít venku kvůli bloku try-catch a PriceChecku
            string name;
            int unitPrice;

            try
            {
                // --- 1. NAČTENÍ DAT PODLE ID TLAČÍTKA ---
                if (buttonId < 1000)
                {
                    var product = ProductService.GetProduct(buttonId);
                    name = product.Name;
                    unitPrice = product.Price;
                }
                else
                {
                    var menu = MenuService.GetMenu(buttonId);
                    name = menu.Name;
                    unitPrice = menu.Price;
                }

                // --- 2. ZPRACOVÁNÍ REŽIMU (KONTROLA CENY VS MARKOVÁNÍ) ---
                if (MainForm.PriceCheck)
                {
                    // Zobrazujeme základní cenu za 1 kus
                    new MessageForm($"Cena položky {name} je {unitPrice} Kč").ShowDialog();
                    MainForm.PriceCheck = false;
                }
                else
                {
                    // Tady se děje to kouzlo. Žádné skupiny, žádné UI. 
                    // Pouze předáme data objektu Order, který si interně vyřeší vše potřebné.
                    if (buttonId < 1000)
                    {
                        order.AddProduct(buttonId, times);
                    }
                    else
                    {
                        order.AddMenu(buttonId, times);
                    }
                }
            }
            catch (ProductNotFoundException)
            {
                MessageBox.Show($"Produkt s ID {buttonId} nebyl v systému nalezen.\nKontaktujte prosím správce.",
                    "Chyba systému", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"Menu s ID {buttonId} nebylo v systému nalezeno.\nKontaktujte prosím správce.",
                    "Chyba systému", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba komunikace s databázovým serverem:\n{ex.Message}",
                    "Chyba sítě", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}