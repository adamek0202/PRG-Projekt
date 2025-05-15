using System;
using System.Windows.Forms;

namespace Pokladna
{
    public partial class Keypad : UserControl
    {
        public int MaxLenght { get; set; }
        public string Value
        {
            get
            {
                return numberTextBox.Text;
            }
        }

        public Keypad()
        {
            InitializeComponent();
        }

        private void kOneButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if ((string)btn.Tag != "C" && numberTextBox.Text.Length < MaxLenght)
            {
                numberTextBox.Text += btn.Tag;
            }
            else if (numberTextBox.Text.Length > 0)
            {
                numberTextBox.Text = numberTextBox.Text.Remove(numberTextBox.Text.Length);
            }
        }
    }
}