using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class DiscountForm : Form
    {
        public int Discount { get; private set; }

        public DiscountForm()
        {
            InitializeComponent();
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
