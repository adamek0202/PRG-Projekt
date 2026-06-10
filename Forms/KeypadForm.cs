using System;
using System.Linq;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class DiscountForm : BaseForm
    {
        public int Discount { get; private set; }

        public DiscountForm()
        {
            InitializeComponent();
        }

        private void KeypadButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if(discountTextBox.Text.Length < 2)
            {
                discountTextBox.Text += btn.Tag;
            }
        }

        private void kDualZeroButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void kRemoveButton_Click(object sender, EventArgs e)
        {
            if(discountTextBox.Text.All(char.IsDigit) && int.Parse(discountTextBox.Text) is > 0 and < 100)
            {
                Discount = int.Parse(discountTextBox.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            
        }
    }
}
