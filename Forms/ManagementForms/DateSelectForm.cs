using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class DateSelectForm : BaseForm
    {
        public DateTime Date { get; private set; }
        
        public DateSelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
