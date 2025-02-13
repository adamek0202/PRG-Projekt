using System;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;

namespace Projekt.Forms
{
    public partial class LoginForm : Form
    {
        private string connectionString = $"Data Source={AppDomain.CurrentDomain.BaseDirectory}users.db;Version=3;";

        public LoginForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            textBox1.Focus();
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
            if (AuthenticateUser(password, out string fullName))
            {
                MainForm.Cashier = fullName;
                new MainForm().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Zadali jste špatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string password, out string fullName)
        {
            fullName = null;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FullName FROM Users WHERE Password = @password";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fullName = reader.GetString(0);
                            return true;
                        }
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
