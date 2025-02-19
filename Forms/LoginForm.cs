using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class LoginForm : Form
    {
        // Uchováváme informaci o tom, jaké tlačítko bylo stisknuto
        private string roleRequired = "";

        public LoginForm(string roleRequired = "")
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            textBox1.Focus();
            this.roleRequired = roleRequired; // Určujeme požadovanou roli
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
            string password = textBox1.Text;

            if (AuthenticateUser(password, out string fullName, out string role))
            {
                if (roleRequired == "manager" && role != "manager")
                {
                    MessageBox.Show($"Zaměstnanec {fullName} s pozicí {role} nemá přístup do manažerské sekce. Pro přístup přiřaďte správnou pozici.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MainForm.Cashier = fullName;

                if (role == "manager" && roleRequired == "manager")
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (role == "crew")
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Zaměstnanec {fullName} s pozicí {role} nemá práva pro vstup do kasy. Pro vstup přiřaďte správnou pozici.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = string.Empty;
                }

            }
            else
            {
                MessageBox.Show("Zadali jste špatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = string.Empty;

            }
        }

        // Metoda pro autentizaci a vrácení role uživatele
        private bool AuthenticateUser(string password, out string fullName, out string role)
        {
            fullName = null;
            role = null;
            string query = "SELECT FullName, Position FROM Users WHERE Password = @password";

            using (SQLiteCommand cmd = new SQLiteCommand(query, DatabaseConnection.Connection))
            {
                cmd.Parameters.AddWithValue("@password", password);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fullName = reader.GetString(0);
                        role = reader.GetString(1); // Předpokládáme, že tabulka Users má sloupec "Role"
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
