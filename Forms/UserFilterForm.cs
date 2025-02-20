using Pokladna;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Projekt.Forms
{
    public partial class UserFilterForm : Form
    {
        public string User { get; private set; }

        public UserFilterForm()
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

        private void UserFilterForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(DatabaseFunctions.GetEmployees("crew"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text != string.Empty)
            {
                User = comboBox1.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
