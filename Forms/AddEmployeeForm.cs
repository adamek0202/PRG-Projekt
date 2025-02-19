using Pokladna;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Projekt.Forms
{
    public partial class AddEmployeeForm : Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

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
                string querry = "SELECT Password FROM Users WHERE Password = @password";
                using(var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {

                    }
                }
            }
        }
    }
}
