using System;
using System.Windows.Forms;

namespace Pokladna.Forms.ProductSelectionForms
{
    internal partial class ChickenForm : BaseForm, IProductSelector
    {
        public int SelectedProductId { get; private set; }

        internal ChickenForm(PosContext context) : base(context)
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChickenButton_Click(object sender, EventArgs e)
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
                Serilog.Log.Warning("Tlačítko {ButtonName} v ChickensForm nemá platné ID produktu v Tagu!", btn?.Name);
            }
        }
    }
}
