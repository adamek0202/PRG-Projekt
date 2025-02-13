using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;

namespace Projekt.Forms
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
            string fullName;
            string role;

            if (AuthenticateUser(password, out fullName, out role))
            {
                if (roleRequired == "manager" && role != "manager")
                {
                    MessageBox.Show("Nemáte dostatečná oprávnění k přístupu do manažerské sekce.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MainForm.Cashier = fullName;

                if (role == "manager" && roleRequired == "manager")
                {
                    // Pokud je to manažer a tlačítko je pro manažera
                    new ManagerForm().ShowDialog();
                }
                else
                {
                    // Otevře se MainForm pro kasíra i manažera
                    new MainForm().ShowDialog();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Zadali jste špatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
