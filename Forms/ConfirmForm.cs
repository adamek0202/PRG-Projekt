using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class ConfirmForm : BaseForm
    {
        private string What;

        public ConfirmForm(string what)
        {
            What = what;
            InitializeComponent();
        }


        private void ConfirmForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"Chcete {What}?";
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
