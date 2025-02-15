using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class TenderedReturnForm : Form
    {
        private int Return;
        public TenderedReturnForm(int money)
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            Return = money;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TenderedReturnForm_Load(object sender, EventArgs e)
        {
            button1.Text = $"Vrátit:\n{Return}Kč\n(klepněte pro uzavření)";
        }
    }
}
