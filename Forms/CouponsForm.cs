using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class CouponsForm : Form
    {
        public CouponsForm()
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

        private void kRemoveButton_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length >= 1)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1); 
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox.Text = string.Empty;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                ProcessCoupon(textBox.Text);
            }
        }

        private bool ProcessCoupon(string code)
        {
            if (textBox.Text.All(char.IsDigit) && textBox.Text.Length == 13)
            {
                MessageBox.Show($"Byl naskenován kupón: {textBox.Text}", "Info");
                textBox.Text = string.Empty;
                return true;
            }
            else
            {
                MessageBox.Show("Nebyl zadán platný kód kopónu", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Clear();
                return false;
            }
        }

        private void kDualZeroButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessCoupon(textBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length < 13)
            {
                var btn = sender as Button;
                textBox.Text += (string)btn.Tag; 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
