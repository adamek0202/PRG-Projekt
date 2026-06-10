using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Pokladna.Forms.ManagementForms;
using Pokladna.Forms.ProductSelectionForms;
using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    // Dědí z naší základní třídy BaseForm
    public partial class StartForm : BaseForm
    {
        // Tady na startu aplikace vytvoříme instanci sdíleného kontextu
        private readonly PosContext _appContext;

        public StartForm()
        {
            InitializeComponent();

            // Inicializace hlavního kontextu pokladny
            _appContext = new PosContext();

            // Nastavení a spuštění hodin
            timer2.Interval = 1000;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Aktualizace času na úvodní obrazovce
            label4.Text = DateTime.Now.ToString("dd.M.yyyy HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Otevře LoginForm a předá mu kontext i požadovanou roli
            using (var loginForm = new LoginForm(_appContext, "cashier"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Visible = false;

                    // Předáme kontext do hlavního okna kasy
                    using (var mainForm = new MainForm(_appContext))
                    {
                        mainForm.ShowDialog();
                    }

                    Visible = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Otevře LoginForm pouze pro manažera
            using (var loginForm = new LoginForm(_appContext, "manager"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Visible = false;

                    // Předáme kontext do manažerské sekce
                    // (ManagerForm pak upravíš tak, aby taky přijímal PosContext)
                    using (var managerForm = new ManagerForm(_appContext))
                    {
                        managerForm.ShowDialog();
                    }

                    Visible = true;
                }
            }
        }
    }
}