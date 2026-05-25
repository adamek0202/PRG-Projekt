using System;
using System.Windows.Forms;

namespace Pokladna.Forms.ProductSelectionForms
{
    internal partial class SupplementsForm : BaseForm, IProductSelector
    {
        public int SelectedProductId { get; private set; }

        internal SupplementsForm(PosContext context) : base(context)
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ItemButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            // Bezpečné parsování ID z Tagu tlačítka
            if (btn?.Tag != null && int.TryParse(btn.Tag.ToString(), out int productId))
            {
                // Žádná statická proměnná. ID si MainForm vytáhne přímo odsud.
                SelectedProductId = productId;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Serilog.Log.Warning("Tlačítko {ButtonName} v SupplementsForm nemá platné ID produktu v Tagu!", btn?.Name);
            }
        }
    }
}
