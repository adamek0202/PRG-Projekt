using Pokladna.Database;
using Pokladna.Dto;
using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    // Dědí z BaseForm, takže má automaticky DWM vypnutí a přístup k _context
    public partial class LoginForm : BaseForm
    {
        private readonly string _roleRequired = "";

        // Konstruktor vyžaduje PosContext a volitelně roli
        internal LoginForm(PosContext context, string roleRequired = "") : base(context)
        {
            InitializeComponent();
            textBox1.Focus();
            _roleRequired = roleRequired;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;

            // Voláme byznys logiku kompletně mimo GUI
            UserSession user = DatabaseFunctions.AuthenticateUser(password);

            if (user != null)
            {
                // Kontrola manažerské role
                if (_roleRequired == "manager" && user.Role != "manager")
                {
                    MessageBox.Show($"Zaměstnanec {user.FullName} s pozicí {user.Role} nemá přístup do manažerské sekce.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Zapíšeme data do kontextu a zresetujeme objednávku
                _context.CurrentCashierName = user.FullName;
                _context.ResetCurrentOrder();

                // Kontrola, zda má vůbec povolený přístup
                //if (user.Role == "crew" || user.Role == "manager")
                //{
                    DialogResult = DialogResult.OK;
                    Close();
                //}
                //else
                //{
                //    MessageBox.Show($"Zaměstnanec {user.FullName} nemá dostatečná práva pro vstup.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    textBox1.Text = string.Empty;
                //}
            }
            else
            {
                MessageBox.Show("Zadali jste špatné heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = string.Empty;
            }
        }
    }
}