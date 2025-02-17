using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
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
            //AddEmployeeForm addForm = new AddEmployeeForm(this);
            //addForm.ShowDialog();
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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
