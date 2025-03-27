using System;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class EditEmployeeForm : Form
    {
        private Employee _employee;
        private ManagerForm _managerForm;

        public EditEmployeeForm(Employee employee, ManagerForm managerForm)
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            _employee = employee;
            _managerForm = managerForm;

            // Naplnění údajů do formuláře
            textBoxFullName.Text = _employee.FullName;
            comboBoxPosition.Text = _employee.Position;
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

        // Kliknutí na tlačítko "Uložit"
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Users SET FullName = @FullName, Position = @Position WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, DatabaseConnection.Connection))
            {
                command.Parameters.AddWithValue("@FullName", textBoxFullName.Text);
                command.Parameters.AddWithValue("@Position", comboBoxPosition.Text);
                command.Parameters.AddWithValue("@Id", _employee.Id);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Zaměstnanec byl úspěšně upraven.", "Hotovo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Aktualizace seznamu v hlavním formuláři
            _managerForm.LoadEmployees();

            // Zavření formuláře
            this.Close();
        }

        // Kliknutí na tlačítko "Zrušit"
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
