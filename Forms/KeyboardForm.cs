using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class KeyboardForm : BaseForm
    {
        public string EnteredText { get; private set; }

        public KeyboardForm()
        {
            InitializeComponent();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if((string)btn.Tag != "Space")
            {
                textBox.Text += (string)btn.Tag;
            }
            else
            {
                textBox.Text += " ";
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if(textBox.Text.Length > 0)
            {
                EnteredText = textBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CloseButton_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void backspaceButton_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1); 
            }
        }
    }
}
