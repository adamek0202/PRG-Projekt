using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms.ProductSelectionForms
{
    internal partial class OthersForm : BaseForm, IProductSelector
    {
        public int SelectedProductId { get; private set; }

        internal OthersForm(PosContext context) : base(context)
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OtherButton_Click(object sender, EventArgs e)
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
                Serilog.Log.Warning("Tlačítko {ButtonName} v OthersForm nemá platné ID produktu v Tagu!", btn?.Name);
            }
        }
    }
}
