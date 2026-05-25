using Pokladna.Database;
using Pokladna.Dto;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class PaymentForm : Form
    {
        private readonly PosContext _context;

        // Konstruktor teď striktně požaduje centrální kontext kasy
        internal PaymentForm(PosContext context)
        {
            InitializeComponent();
            _context = context;

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

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            NativeFunctions.DisableVisualStyles(listView1);

            // Cenu taháme bezpečně z datového modelu
            sumLabel.Text = $"Celkem: {_context.CurrentOrder.GetTotalPrice()} Kč";

            // Pokud v platebním okně chceš zobrazit přehled nákupu, 
            // naplníš lokální listView1 přímo z datové kolekce objednávky
            LoadListViewFromOrder();
        }

        private void LoadListViewFromOrder()
        {
            listView1.Items.Clear();
            listView1.Groups.Clear(); // Přidáno: Vyčistíme i staré skupiny

            // 1. Vykreslíme položky z košíku se všemi komponentami a poznámkami
            foreach (var item in _context.CurrentOrder.Items)
            {
                var group = new ListViewGroup();
                listView1.Groups.Add(group);

                if (item.IsMenu)
                {
                    // --- KRESLÍME MENU ---
                    var menuRow = new ListRow(new string[] { item.Name, $"{item.TotalPrice} Kč", item.Count.ToString() }, group)
                    {
                        BackColor = System.Drawing.Color.Orange,
                        Tag = "root",
                        CouponID = item.CouponId
                    };
                    listView1.Items.Add(menuRow);

                    // Obecné poznámky k celému menu
                    foreach (var note in item.Notes)
                    {
                        var noteRow = new ListRow($"  * {note}", group) { BackColor = System.Drawing.Color.SpringGreen, Tag = "note" };
                        listView1.Items.Add(noteRow);
                    }

                    // Komponenty menu (žluté řádky)
                    foreach (var compName in item.ComponentNames)
                    {
                        var componentRow = new ListRow(compName, group) { BackColor = System.Drawing.Color.Yellow };
                        listView1.Items.Add(componentRow);

                        // Poznámky patřící konkrétní komponentě
                        if (item.ComponentNotes != null && item.ComponentNotes.TryGetValue(compName, out var compNotes))
                        {
                            foreach (var compNote in compNotes)
                            {
                                var noteRow = new ListRow($"  * {compNote}", group) { BackColor = System.Drawing.Color.SpringGreen, Tag = "note" };
                                listView1.Items.Add(noteRow);
                            }
                        }
                    }
                }
                else
                {
                    // --- KRESLÍME BEŽNÝ PRODUKT / KUPÓN ---
                    var productRow = new ListRow(new string[] { item.Name, $"{item.TotalPrice} Kč", item.Count.ToString() }, group)
                    {
                        BackColor = System.Drawing.Color.Orange,
                        Tag = "",
                        CouponID = item.CouponId
                    };
                    listView1.Items.Add(productRow);

                    // Obecné poznámky k produktu
                    foreach (var note in item.Notes)
                    {
                        var noteRow = new ListRow($"  * {note}", group) { BackColor = System.Drawing.Color.SpringGreen, Tag = "note" };
                        listView1.Items.Add(noteRow);
                    }
                }
            }

            // 2. Pokud je na objednávce aplikovaná manuální sleva, přihodíme zelený řádek nakonec
            if (_context.CurrentOrder.ManualDiscountPercentage > 0)
            {
                // Spočítáme čistou cenu položek před slevou (klasický LINQ Sum)
                int basePrice = _context.CurrentOrder.Items.Sum(item => item.TotalPrice);

                // Hodnota slevy v Kč je rozdíl původní ceny a ceny po slevě
                int discountInKcz = basePrice - _context.CurrentOrder.GetTotalPrice();

                // Pro slevový řádek vytvoříme samostatnou skupinu na konci, aby to vizuálně nerozbilo mřížku
                var discountGroup = new ListViewGroup("Sleva");
                listView1.Groups.Add(discountGroup);

                var discountRow = new ListRow(new string[] {
            $"Sleva ({_context.CurrentOrder.ManualDiscountPercentage}%)",
            $"-{discountInKcz} Kč",
            "1"
        }, discountGroup)
                {
                    BackColor = System.Drawing.Color.Lime
                };

                listView1.Items.Add(discountRow);
            }
        }


        private void ExactCashButton_Click(object sender, EventArgs e)
        {
            // Přesná hotovost: Zákazník dal přesně tolik, kolik měl platit
            _context.CurrentOrder.PaymentMethod = Payments.Cash;
            _context.CurrentOrder.AmountTendered = _context.CurrentOrder.GetTotalPrice();

            // Zavoláme finální zpracování tržby
            FinalizeTransaction();
        }

        private void CashButton_Click(object sender, EventArgs e)
        {
            // Zákazník platí hotovostí a zadal částku na klávesnici
            if (int.TryParse(PayedTextBox.Text, out int payedAmount))
            {
                int totalPrice = _context.CurrentOrder.GetTotalPrice();

                if (payedAmount >= totalPrice)
                {
                    _context.CurrentOrder.PaymentMethod = Payments.Cash;
                    _context.CurrentOrder.AmountTendered = payedAmount;

                    // Otevře okno "Vrátit: X Kč"
                    new TenderedReturnForm(payedAmount - totalPrice).ShowDialog();

                    FinalizeTransaction();
                }
            }
        }

        private void CardButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            // Otevře animaci komunikace s terminálem
            using (var cardForm = new CardPaymentProcessForm())
            {
                if (cardForm.ShowDialog() == DialogResult.OK)
                {
                    _context.CurrentOrder.PaymentMethod = (string)btn.Tag == "FoodCard" ? Payments.FoodCard : Payments.Card;
                    _context.CurrentOrder.AmountTendered = _context.CurrentOrder.GetTotalPrice(); // Kartou se platí vždy přesně

                    FinalizeTransaction();
                }
            }
        }

        /// <summary>
        /// Centrální místo, které po úspěšném zaplacení odpálí zápisy a tisk.
        /// </summary>
        private void FinalizeTransaction()
        {
            // 1. KROK: Zápis do databáze (Kritická operace)
            try
            {
                // Zapíšeme tržbu do MySQL (tady předáváš celý objekt objednávky)
                DatabaseFunctions.RecordSale(_context.CurrentOrder);

                // DatabaseFunctions.SendOrderToKitchen(_context.CurrentOrder);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Kritické selhání při zápisu transakce do DB. Účtenka č. {ReceiptId}", _context.CurrentOrder.ReceiptId);
                MessageBox.Show("Chyba při ukládání platby do databáze! Zkontrolujte připojení k serveru.\nData nebyla uložena.", "Kritická chyba DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. KROK: Tisk účtenky (Rozbalení DTO do upravené metody)
            try
            {
                // Tady mapujeme vlastnosti tvého Order DTO přímo do tiskové metody, 
                // která nově očekává List<SaleItemDto>
                Receipt.PrintReceipt(
                    (System.Collections.Generic.List<SaleItemDto>)_context.CurrentOrder.Items,       // List<SaleItemDto> (tvůj košík položek)
                    _context.CurrentOrder.PaymentMethod, // Payments enum (Cash / Card / FoodCard)
                    _context.CurrentOrder.GetTotalPrice(),  // Celková cena po slevě
                    _context.CurrentOrder.AmountTendered,        // Hotovost od zákazníka (tendered)
                    _context.CurrentOrder.ManualDiscountPercentage,    // Výše uplatněné slevy
                    _context.CurrentOrder.ReceiptId,   // Číslo účtenky z DB
                    _context.CurrentOrder.CashierName, // Jméno přihlášeného uživatele
                    _context.CurrentOrder.IsTakeAway      // Příznak V restauraci / S sebou
                );
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "Účtenka č. {ReceiptId} byla v DB uložena, ale selhal její fyzický tisk.", _context.CurrentOrder.ReceiptId);
                MessageBox.Show("Platba byla úspěšně uložena, ale nepodařilo se vytisknout účtenku.\nZkontrolujte tiskárnu (papír, kabely).", "Varování tiskárny", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. KROK: Úspěšné uzavření formuláře
            DialogResult = DialogResult.OK;
            Close();
        }

        private void KRemoveButton_Click(object sender, EventArgs e)
        {
            // Pokud v textovém poli něco je, smaže to poslední znak (BackSpace)
            if (PayedTextBox.Text.Length >= 1)
            {
                PayedTextBox.Text = PayedTextBox.Text.Remove(PayedTextBox.Text.Length - 1);
            }
        }

        private void KeypadButton_Click(object sender, EventArgs e)
        {
            // Ochrana, aby uživatel nezačal bušit nekonečné množství čísel
            if (PayedTextBox.Text.Length < 5)
            {
                var btn = sender as Button;
                if (btn?.Tag != null)
                {
                    // Přidá znak z Tagu tlačítka (např. "5") do textového pole
                    PayedTextBox.Text += btn.Tag.ToString();
                }
            }
        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            using (var df = new DiscountForm())
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    // 1. Zadáme slevu přímo do datového modelu objednávky
                    _context.CurrentOrder.ApplyManualDiscount(df.Discount);

                    // 2. Pokud chceš, aby byl řádek se slevou vidět i vizuálně v listView1
                    // upravíme metodu LoadListViewFromOrder(), viz níže.
                    LoadListViewFromOrder();

                    // 3. Aktualizujeme celkovou cenu na labelu (která už v sobě má slevu započtenou)
                    sumLabel.Text = $"Celkem: {_context.CurrentOrder.GetTotalPrice()} Kč";
                }
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}