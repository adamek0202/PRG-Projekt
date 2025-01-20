using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;

namespace Projekt.Forms
{
    public partial class CardPaymentProcessForm : Form
    {
        public CardPaymentProcessForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DWMNCRENDERINGPOLICY renderingPolicy = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            int hr;
            hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void CardPaymentProcessForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
