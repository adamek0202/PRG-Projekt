using System;
using System.Runtime.InteropServices;

namespace Pokladna.Forms
{
    public partial class CardPaymentProcessForm : BaseForm
    {
        public CardPaymentProcessForm()
        {
            InitializeComponent();
        }

        private void CardPaymentProcessForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Schváleno";
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
