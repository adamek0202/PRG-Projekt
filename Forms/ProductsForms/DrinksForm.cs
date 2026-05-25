using System;
using System.Windows.Forms;

namespace Pokladna.Forms.ProductSelectionForms
{
    // 1. Změníme dědičnost z Form na BaseForm
    // 2. Přidáme implementaci rozhraní IProductSelector
    public partial class DrinksForm : BaseForm, IProductSelector
    {
        // Vlastnost, kterou vyžaduje rozhraní IProductSelector pro předání ID zpět do MainFormu
        public int SelectedProductId { get; private set; }

        // Konstruktor přijímá PosContext a předává ho pomocí : base(context) do BaseFormu
        internal DrinksForm(PosContext context) : base(context)
        {
            InitializeComponent();
            // ReallyCenterToScreen(this) už nemusíš volat, řeší to BaseForm!
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DrinkButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn?.Tag != null && int.TryParse(btn.Tag.ToString(), out int productId))
            {
                // ŽÁDNÝ STATICKÝ ZÁPIS! 
                // ID uložíme do vlastnosti instance a MainForm si ho sám přečte.
                SelectedProductId = productId;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Pokud bys měl v Tagu tlačítka překlep nebo prázdno, zalogujeme to
                Serilog.Log.Warning("Tlačítko {ButtonName} v DrinksForm nemá platné ID produktu v Tagu!", btn?.Name);
            }
        }
    }
}