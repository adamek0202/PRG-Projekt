using Pokladna.Exceptions;
using Pokladna.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class CouponForm : BaseForm
    {
        internal Coupon Coupon { get; private set; }

        public CouponForm()
        {
            InitializeComponent();
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

        private void ProcessCoupon(string couponCode)
        {
            // Použijeme parametr 'code', který do metody přišel. 
            // Pokud by byl prázdný, ořízneme text z textboxu jako zálohu.;

            // Validace formátu EAN-13 (fastfoodové kupóny z appky/papíru)
            if (couponCode.All(char.IsDigit) && couponCode.Length == 13)
            {
                try
                {
                    // Voláme novou CouponService místo DatabaseFunctions
                    var coupon = CouponService.GetCoupon(couponCode);

                    MessageBox.Show($"Byl naskenován kupón: {coupon.Name}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Uložíme do vlastnosti ve Formu, ať k němu má MainForm přístup
                    this.Coupon = coupon;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (CouponValidationException ex)
                {
                    // Naše nová specifická výjimka (prošlý kupón, vyčerpaný počet použití atd.)
                    new MessageForm(ex.Message).ShowDialog();
                }
                catch (Exception ex)
                {
                    // Pro strach z nečekaných chyb (např. pád SQLite, neošetřený formát v DB)
                    new MessageForm($"Systémová chyba při načítání kupónu: {ex.Message}").ShowDialog();
                }
                finally
                {
                    // Ať už to projde, nebo vyletí chyba, textbox vyčistíme vždycky na jednom místě
                    textBox.Clear();
                }
            }
            else
            {
                new MessageForm("Neplatný formát kupónu.").ShowDialog();
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
