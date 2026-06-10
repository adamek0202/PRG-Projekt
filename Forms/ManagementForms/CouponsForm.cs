using Pokladna.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace Pokladna.Forms.ManagementForms
{
    public partial class CouponsForm : Form
    {
        private readonly List<CommandBinding> _bindings = new();

        public CouponsForm()
        {
            InitializeComponent();
            NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);

            // Ideální načíst data hned při startu formuláře
            RefreshListView();
        }

        private void listViewWithScrollBar1_DoubleClick(object sender, EventArgs e)
        {
            EditCoupon();
        }

        /// <summary>
        /// Přidání úplně nového kupónu
        /// </summary>
        private void AddCoupon()
        {
            // Voláme bezparametrický konstruktor pro NOVÝ kupón
            using (var cef = new CouponEditForm())
            {
                if (cef.ShowDialog() == DialogResult.OK)
                {
                    // Tady vytáhneš z formuláře ten nově poskládaný kupón
                    // (např. přes vlastnost cef.CreatedCoupon) a uložíš ho do DB:
                    // CouponService.SaveNewCoupon(cef.CreatedCoupon);

                    RefreshListView();
                }
            }
        }

        /// <summary>
        /// Úprava existujícího kupónu
        /// </summary>
        private void EditCoupon()
        {
            if (listViewWithScrollBar1.SelectedItems.Count == 1)
            {
                // Předpokládám, že klíč (Code/EAN) kupónu máš schovaný v prvním sloupci nebo v Tagu řádku
                string selectedCouponCode = listViewWithScrollBar1.SelectedItems[0].Text;

                try
                {
                    // Načteme reálná a validní data přímo z DB přes naši novou service
                    Coupon existingCoupon = CouponService.GetCoupon(selectedCouponCode);

                    // Předáme existující kupón do konstruktoru pro ÚPRAVU
                    using (var cef = new CouponEditForm(existingCoupon))
                    {
                        if (cef.ShowDialog() == DialogResult.OK)
                        {
                            // Vytáhneš upravená data z okna a uložíš do DB
                            // CouponService.ModifyCoupon(cef.UpdatedCoupon);

                            RefreshListView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepodařilo se načíst kupón pro editaci: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Smazání kupónu
        /// </summary>
        private void RemoveCoupon()
        {
            if (listViewWithScrollBar1.SelectedItems.Count == 1)
            {
                string selectedCouponCode = listViewWithScrollBar1.SelectedItems[0].Text;
                string couponName = listViewWithScrollBar1.SelectedItems[0].SubItems[1].Text; // předpokládám druhý sloupec název

                var result = MessageBox.Show($"Opravdu chcete smazat kupón '{couponName}' ({selectedCouponCode})?",
                    "Potvrdit smazání", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // CouponService.DeleteCoupon(selectedCouponCode);
                        RefreshListView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Kupón se nepodařilo smazat: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Pomocná metoda pro znovunačtení všech kupónů do tabulky
        /// </summary>
        private void RefreshListView()
        {
            listViewWithScrollBar1.Items.Clear();

            try
            {
                // Tady v budoucnu zavoláš metodu ze service vrstvy, 
                // která ti vrátí seznam všech kupónů, např. List<Coupon> nebo DataTable
                //
                // var coupons = CouponService.GetAllCoupons();
                // foreach(var coupon in coupons) { ... naplnění listView ... }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání seznamu kupónů: {ex.Message}", "Chyba databáze", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}