using System;

namespace Pokladna.Forms
{
    public partial class TenderedReturnForm : BaseForm
    {
        private int Return;
        public TenderedReturnForm(int money)
        {
            InitializeComponent();
            Return = money;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TenderedReturnForm_Load(object sender, EventArgs e)
        {
            button1.Text = $"Vrátit:\n{Return}Kč\n(klepněte pro uzavření)";
        }
    }
}
