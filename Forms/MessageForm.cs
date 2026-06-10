using System;
using System.Runtime.InteropServices;

namespace Pokladna.Forms
{
    public partial class MessageForm : BaseForm
    {
        public MessageForm(string message)
        {
            InitializeComponent();
            label1.Text = message;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
