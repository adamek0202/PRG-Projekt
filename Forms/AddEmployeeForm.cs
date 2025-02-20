using Pokladna;
using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Projekt.Forms
{
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            if(textBoxFullName.Text != string.Empty && comboBoxPosition.Text != string.Empty && passwordTextBox.Text != string.Empty)
            {
                if(DatabaseFunctions.CreateEmployee(textBoxFullName.Text, passwordTextBox.Text, comboBoxPosition.Text))
                {
                    MessageBox.Show("Zaměstnanec byl úspěšně přidán", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                MessageBox.Show("Nepodařilo se přídat zaměstnance\nTento zaměstnanec již v systému existuje", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
