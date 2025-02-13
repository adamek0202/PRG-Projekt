using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projekt.BasicTheme;

namespace Projekt.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
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

        private void button1_Click(object sender, EventArgs e)
        {
            switch (textBox1.Text)
            {
                case "chalupnicek":
                    MainForm.Cashier = "Jakub Chalupníček";
                    new MainForm().Show();
                    break;

                case "karoch":
                    MainForm.Cashier = "Štěpán Karoch";
                    new MainForm().Show();
                    break;

                case "benda":
                    MainForm.Cashier = "Adam Benda";
                    new MainForm().Show();
                    break;

                case "manazer":
                    MainForm.Cashier = "Květa Prosová";
                    new MainForm().Show();
                    break;
                default: MessageBox.Show("Zadali jste špatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
            };
        }
    }
}
