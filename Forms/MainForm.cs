using Pokladna.Database;
using Pokladna.Dto;
using Pokladna.Forms;
using Pokladna.Forms.Controls;
using Pokladna.Forms.ProductSelectionForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    internal partial class MainForm : Form
    {
        private readonly PosContext _context;
        private bool _replace = false;
        public static bool PriceCheck { get; set; } = false;
        private int _multiplier = 1;

        // Konstruktor dostane hotový naplněný kontext z Program.cs
        internal MainForm(PosContext context)
        {
            InitializeComponent();
            Height = 1080;

            _context = context;
        }

        internal List<ListRow> ListViewData
        {
            get
            {
                List<ListRow> data = new List<ListRow>();
                foreach (ListRow item in listView.Items)
                {
                    data.Add((ListRow)item);
                }
                return data;
            }
        }
        private void UpdateOrderInterface()
        {
            listView.Items.Clear();
            listView.Groups.Clear();

            foreach (var item in _context.CurrentOrder.Items)
            {
                var group = new ListViewGroup();
                group.Tag = item; // Skupina drží odkaz na SaleItemDto
                listView.Groups.Add(group);

                if (item.IsMenu)
                {
                    // --- KRESLÍME MENU ---
                    var menuRow = new ListRow(new string[] { item.Name, $"{item.TotalPrice} Kč", item.Count.ToString() }, group)
                    {
                        BackColor = Color.Orange,
                        Tag = "root",
                        CouponID = item.CouponId
                    };
                    listView.Items.Add(menuRow);

                    // Obecné poznámky k celému menu
                    foreach (var note in item.Notes)
                    {
                        var noteRow = new ListRow($"  * {note}", group) { BackColor = Color.SpringGreen, Tag = "note" };
                        listView.Items.Add(noteRow);
                    }

                    // Komponenty menu (procházíme původní List<string>)
                    foreach (var compName in item.ComponentNames)
                    {
                        var componentRow = new ListRow(compName, group)
                        {
                            BackColor = Color.Yellow,
                            // Do Tagu si uložíme pomocný kontext, abychom věděli název komponenty
                            Tag = new ComponentContext { ComponentName = compName }
                        };
                        listView.Items.Add(componentRow);

                        // Vytáhneme a vykreslíme poznámky patřící této komponentě z nového slovníku
                        if (item.ComponentNotes != null && item.ComponentNotes.TryGetValue(compName, out var compNotes))
                        {
                            foreach (var compNote in compNotes)
                            {
                                var noteRow = new ListRow($"  * {compNote}", group) { BackColor = Color.SpringGreen, Tag = "note" };
                                listView.Items.Add(noteRow);
                            }
                        }
                    }
                }
                else
                {
                    // --- KRESLÍME PRODUKT / KUPÓN ---
                    var productRow = new ListRow(new string[] { item.Name, $"{item.TotalPrice} Kč", item.Count.ToString() }, group)
                    {
                        BackColor = Color.Orange,
                        Tag = "",
                        CouponID = item.CouponId
                    };
                    listView.Items.Add(productRow);

                    foreach (var note in item.Notes)
                    {
                        var noteRow = new ListRow($"  * {note}", group) { BackColor = Color.SpringGreen, Tag = "note" };
                        listView.Items.Add(noteRow);
                    }
                }
            }

            sumLabel.Text = $"Celkem {_context.CurrentOrder.GetTotalPrice()} Kč";

            if (listView.Items.Count > 0)
            {
                listView.Items[listView.Items.Count - 1].Selected = true;
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }
        }

        // Malá pomocná třída, kterou hodíme na konec souboru Formu, abychom identifikovali řádek komponenty
        internal class ComponentContext
        {
            public string ComponentName { get; set; } = string.Empty;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DatabaseConnection.CloseConnection();
        }

        #region Pohyb
        private void SelectPreviousItem()
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int currentIndex = listView.SelectedIndices[0];
                if (currentIndex > 0)
                {
                    listView.Items[currentIndex].Selected = false;
                    listView.Items[currentIndex - 1].Selected = true;
                    listView.Items[currentIndex - 1].EnsureVisible();
                }
            }
            else if (listView.Items.Count > 0)
            {
                listView.Items[listView.Items.Count - 1].Selected = true;
                listView.Items[listView.Items.Count - 1].EnsureVisible();
            }
        }

        private void SelectNextItem()
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int currentIndex = listView.SelectedIndices[0];
                if (currentIndex < listView.Items.Count - 1)
                {
                    listView.Items[currentIndex].Selected = false;
                    listView.Items[currentIndex + 1].Selected = true;
                    listView.Items[currentIndex + 1].EnsureVisible();
                }
            }
            else if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].EnsureVisible();
            }
        }
        #endregion Posun

        private void DownButton_Click(object sender, EventArgs e)
        {
            SelectNextItem();
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            SelectPreviousItem();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            // Kontrola, zda je vůbec něco vybráno a zda to má skupinu
            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Group != null)
            {
                if (new ConfirmForm("tuto položku stornovat").ShowDialog() == DialogResult.OK)
                {
                    // Zjistíme index vybrané skupiny v ListView
                    var selectedGroup = listView.SelectedItems[0].Group;
                    int groupIndex = listView.Groups.IndexOf(selectedGroup);

                    if (groupIndex != -1)
                    {
                        // 1. Smažeme položku z datového modelu objednávky
                        _context.CurrentOrder.RemoveItem(groupIndex);

                        // 2. Kompletně překreslíme UI z nových dat (samy zmizí komponenty i se přepočítá cena)
                        UpdateOrderInterface();
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Místo kontroly prvků na obrazovce se rovnou podíváme, jestli máme něco v objednávce
            if (_context.CurrentOrder.Items.Count > 0)
            {
                if (new ConfirmForm("tento účet stornovat").ShowDialog() == DialogResult.OK)
                {
                    // 1. Vymažeme všechna data objednávky z paměti RAM
                    _context.CurrentOrder.Clear();

                    // 2. Překreslíme UI – tabulka se smaže a celková cena skočí na "Celkem 0 Kč"
                    UpdateOrderInterface();
                }
            }
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                //_context.CurrentOrder. = true;
            }
        }

        private void ItemButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // 1. Zpracujeme logiku přes košík v paměti (třída ButtonHandler už neví nic o WinForms)
            ButtonHandler.HandleButtonPress(_context.CurrentOrder, Convert.ToInt32(btn.Tag), _multiplier);

            // 2. Necháme formulář, ať se pasivně překreslí podle nových dat
            UpdateOrderInterface();

            _multiplier = 1;
            multiplierLabel.Text = $"×{_multiplier}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NativeFunctions.DisableVisualStyles(listView);
            TopLevel = true;
            label1.Text += _context.CurrentCashierName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(new ConfirmForm("skončit").ShowDialog() == DialogResult.OK)
            {
                Close();
            }
        }

        private void PaymentButton_Click(object sender, EventArgs e)
        {
            // 1. Validace dat – markujeme z dat, ne z prvků na obrazovce
            if (_context.CurrentOrder.Items.Count == 0 || _context.CurrentOrder.GetTotalPrice() == 0)
            {
                return;
            }

            // 2. Předáme platebnímu formuláři CELÝ kontext kasy
            using (var paymentForm = new PaymentForm(_context))
            {
                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
                    // Platba proběhla úspěšně! (Zápis do DB a tisk se vyřešily uvnitř)

                    // 3. Posuneme číslo účtenky o 1 dál
                    _context.IncrementReceiptId();

                    // 4. Vyčistíme starý nákupní koš a založíme čistou objednávku s novým ID
                    _context.ResetCurrentOrder();

                    // 5. Pasivně překreslíme prázdnou kasu pro dalšího zákazníka
                    UpdateOrderInterface();
                }
            }
        }

        private void MultiplierButtons_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            string digitPressed = btn.Tag?.ToString() ?? "";

            if (digitPressed == "X")
            {
                _multiplier = 0;
                multiplierLabel.Text = $"×{_multiplier}";
                return;
            }

            // Pokud se pokladní pokusí parsovat nějaký nesmysl, raději to sychrujeme
            if (int.TryParse(digitPressed, out int digit))
            {
                // Matematické posunutí řádu: např. 1 * 10 + 5 = 15
                int newMultiplier = (_multiplier * 10) + digit;

                // Ochrana na maximálně dvojciferné číslo (0-99)
                if (newMultiplier > 99)
                {
                    _multiplier = 0;
                }
                else
                {
                    _multiplier = newMultiplier;
                }

                multiplierLabel.Text = $"×{_multiplier}";
            }
        }

        private void BagButton_Click(object sender, EventArgs e)
        {
            const int bagProductId = 803; // Sem doplň reálné ID produktu "Taška" z DB

            _context.CurrentOrder.AddProduct(bagProductId, 1);
            UpdateOrderInterface();
        }

        private void ExternalFormsButtons_Click(object sender, EventArgs e)
        {
            // Změněno na BaseForm, abychom mohli jednotně pracovat s kontextem
            BaseForm selectedForm = null;
            var btn = sender as Button;

            if (btn?.Tag == null) return;

            switch (btn.Tag.ToString())
            {
                // VŠEM oknům předáme do konstruktoru náš _context
                case "BSmarts":
                    selectedForm = new BSmartsForm(_context);
                    break;
                case "Drinks":
                    selectedForm = new DrinksForm(_context);
                    break;
                case "Chicken":
                    selectedForm = new ChickenForm(_context);
                    break;
                case "Supplements":
                    selectedForm = new SupplementsForm(_context);
                    break;
                case "Desserts":
                    selectedForm = new DessertsForm(_context);
                    break;
                case "Others":
                    selectedForm = new OthersForm(_context);
                    break;
                default:
                    return;
            }

            // Otevřeme podformulář
            if (selectedForm.ShowDialog() == DialogResult.OK)
            {
                // Bezpečně zkontrolujeme, zda okno implementuje IProductSelector
                if (selectedForm is IProductSelector selector)
                {
                    // Vytáhneme ID produktu přímo z instance zavřeného okna
                    int productId = selector.SelectedProductId;

                    // Zpracujeme stisk tlačítka s reálným ID a lokálním násobičem
                    ButtonHandler.HandleButtonPress(_context.CurrentOrder, productId, _multiplier);

                    // Překreslíme hlavní obrazovku kasy
                    UpdateOrderInterface();
                }
                else
                {
                    Serilog.Log.Error("Formulář {FormName} sice vrátil DialogResult.OK, ale neimplementuje IProductSelector!", selectedForm.Name);
                }

                // Resetujeme násobič na hlavní obrazovce
                _multiplier = 1;
                multiplierLabel.Text = $"×{_multiplier}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            // 1. Změníme hodnotu v objektu objednávky
            _context.CurrentOrder.IsTakeAway = !_context.CurrentOrder.IsTakeAway;

            // 2. Aktualizujeme text tlačítka podle nové hodnoty
            // Pokud je IsTakeAway == true, chceme, aby tlačítko nabízelo přepnutí na "Tady"
            btn.Text = _context.CurrentOrder.IsTakeAway ? "Tady" : "S sebou";

            // 3. Aktualizujeme popisek
            LocationLabel.Text = _context.CurrentOrder.IsTakeAway ? "S sebou" : "Tady";
        }

        private void ManagerButton_Click(object sender, EventArgs e)
        {
            //if (new LoginForm("manager").ShowDialog() == DialogResult.OK)
            //{
            //    new ManagerForm().ShowDialog();
            //}
        }

        private void priceAskButton_Click(object sender, EventArgs e) { PriceCheck = true; }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 1 || listView.SelectedItems[0].Group == null) return;

            var selectedItem = listView.SelectedItems[0];
            var currentGroup = selectedItem.Group;

            if (currentGroup.Tag is not SaleItemDto parentItem) return;

            bool isSelectedNote = selectedItem.Tag is string tagStr && tagStr == "note";

            // PRVNÍ PŘÍPAD: Přidání poznámky
            if (!isSelectedNote)
            {
                var noteForm = new ItemNotesForm();
                if (noteForm.ShowDialog() == DialogResult.OK)
                {
                    // Zjistíme, jestli stojíme na žlutém řádku komponenty menu
                    if (selectedItem.Tag is ComponentContext compContext)
                    {
                        // Inicializujeme slovník a list v DTO, pokud ještě neexistují
                        parentItem.ComponentNotes ??= new(StringComparer.OrdinalIgnoreCase);
                        if (!parentItem.ComponentNotes.ContainsKey(compContext.ComponentName))
                        {
                            parentItem.ComponentNotes[compContext.ComponentName] = new List<string>();
                        }

                        // Přidáme poznámku ke konkrétní komponentě
                        parentItem.ComponentNotes[compContext.ComponentName].Add(noteForm.Note);
                    }
                    else
                    {
                        // Přidáme obecnou poznámku k celému produktu/menu do původního Listu
                        parentItem.Notes.Add(noteForm.Note);
                    }

                    UpdateOrderInterface();
                }
            }
            // DRUHÝ PŘÍPAD: Smazání poznámky
            else
            {
                if (new ConfirmForm("tuto poznámku odstranit").ShowDialog() == DialogResult.OK)
                {
                    string textToRemove = selectedItem.Text.Replace("  * ", "").Trim();

                    // 1. Zkusíme smazat z obecných poznámek
                    if (parentItem.Notes.Contains(textToRemove))
                    {
                        parentItem.Notes.Remove(textToRemove);
                    }
                    // 2. Pokud tam není, prohledáme slovník poznámek u komponent
                    else if (parentItem.ComponentNotes != null)
                    {
                        foreach (var pair in parentItem.ComponentNotes)
                        {
                            if (pair.Value.Contains(textToRemove))
                            {
                                pair.Value.Remove(textToRemove);
                                break;
                            }
                        }
                    }

                    UpdateOrderInterface();
                }
            }
        }

        private void CouponsButton_Click(object sender, EventArgs e)
        {
            var cf = new CouponForm();
            if (cf.ShowDialog() == DialogResult.OK)
            {
                // 1. Narveme kupón se všemi jeho podpoložkami rovnou do objednávky
                _context.CurrentOrder.AddCoupon(cf.Coupon);

                // 2. Necháme UI pasivně překreslit stav z dat
                UpdateOrderInterface();
            }
        }

        private void giftCardButton_Click(object sender, EventArgs e)
        {
            var grf = new GiftCardReadForm(_context);
            if(grf.ShowDialog() == DialogResult.OK)
            {
                new GiftCardInfoForm().ShowDialog();
            }
        }
    }
}
