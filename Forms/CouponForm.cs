using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;
using static Pokladna.Forms.ItemSalesForm;

namespace Pokladna.Forms
{
    public partial class CouponForm : Form
    {
        internal Coupon Coupon { get; private set; }

        public CouponForm()
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

        private void ProcessCoupon(string code)
        {
            if (textBox.Text.All(char.IsDigit) && textBox.Text.Length == 13)
            {
                try
                {
                    var coupon = DatabaseFunctions.GetCoupon(textBox.Text);
                    MessageBox.Show($"Byl naskenován kupón: {coupon.Name}", "Info");
                    Coupon = coupon;
                    textBox.Text = string.Empty;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (CouponException ex)
                {
                    textBox.Text = string.Empty;
                    new MessageForm(ex.Message).ShowDialog();
                }
                catch (EmptyDatasetException ex)
                {
                    new MessageForm(ex.Message).ShowDialog();
                }
            }
            else
            {
                new MessageForm("Neplatný kupón").ShowDialog();
                textBox.Clear();
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
