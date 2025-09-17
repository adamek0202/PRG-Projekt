using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            elementHost1.Child = new Ribbon();
            ReallyCenterToScreen(this);
            NativeFunctions.DisableVisualStyles(listBoxEmployees);
            LoadEmployees();
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

        // Načtení zaměstnanců do ListBoxu
        public void LoadEmployees()
        {
            listBoxEmployees.Items.Clear();
            string query = "SELECT FullName, Position FROM Users"; // Zkontrolujte, že tabulka odpovídá vaší databázi

            using (var command = new SQLiteCommand(query, DatabaseConnection.Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string displayText = $"{reader["FullName"]} ({reader["Position"]})";
                    listBoxEmployees.Items.Add(displayText);
                }
            }
        }

        // Kliknutí na tlačítko "Přidat zaměstnance"
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var aef = new AddEmployeeForm();
            aef.MdiParent = this;
            if (aef.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        // Kliknutí na tlačítko "Upravit zaměstnance"
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedItem != null)
            {
                string selectedText = listBoxEmployees.SelectedItem.ToString();
                string fullName = selectedText.Split('(')[0].Trim(); // Oddělení jména

                Employee selectedEmployee = GetEmployeeByFullName(fullName);
                if (selectedEmployee != null)
                {
                    EditEmployeeForm editForm = new EditEmployeeForm(selectedEmployee, this);
                    editForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vyberte zaměstnance k úpravě.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Dvojklik na ListBox otevře editační formulář
        private void listBoxEmployees_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit_Click(sender, e);
        }

        // Metoda pro získání zaměstnance z databáze
        private Employee GetEmployeeByFullName(string fullName)
        {
            string query = "SELECT * FROM Users WHERE FullName = @FullName";
            Employee employee = null;

            using (var command = new SQLiteCommand(query, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("@FullName", fullName);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Position = reader["Position"].ToString()
                        };
                    }
                }
            }

            return employee;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ItemsSalesButton_Click(object sender, EventArgs e)
        {
            new ItemSalesForm().ShowDialog();
        }

        private void TransactionsButton_Click(object sender, EventArgs e)
        {
            new SalesForm().ShowDialog();
        }

        private void RemoveUserButton_Click(object sender, EventArgs e)
        {
            if (listBoxEmployees.SelectedItems.Count >= 1)
            {
                if (MessageBox.Show($"Opravdu chcete smazat zaměstnance {RemoveTextInParentheses(listBoxEmployees.SelectedItem.ToString())}?", "Dotaz", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (DatabaseFunctions.RemoveEmployee(RemoveTextInParentheses(listBoxEmployees.SelectedItem.ToString())))
                    {
                        MessageBox.Show("Zaměstnanec byl úspěšně smazán", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadEmployees();  
                }
            }
        }

        private string RemoveTextInParentheses(string input)
        {
            return Regex.Replace(input, @"\s*\(.*?\)", "");
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }
    }
}
