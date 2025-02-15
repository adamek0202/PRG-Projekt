using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            timer2.Interval = 1000;
            timer2.Start();
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("dd.M.yyyy HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Otevře LoginForm pro přihlášení jak pro pokladního, tak pro manažera
            new LoginForm().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Otevře LoginForm pouze pro manažera
            new LoginForm("manager").ShowDialog();
        }
    }
}
