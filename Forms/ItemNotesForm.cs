using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public partial class ItemNotesForm : BaseForm
    {
        public string Note { get; private set; }

        private string[] Notes = {"Extra majoneza", "Bez rajcat", "Bez majonezy", "Extra sul", "Bez salatu", "Extra omacka", "Bez soli", "Bez omacky"};

        public ItemNotesForm()
        {
            InitializeComponent();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NoteButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Note = Notes[int.Parse((string)btn.Tag) - 1];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void otherButton_Click(object sender, EventArgs e)
        {
            var keyboardForm = new KeyboardForm();
            if(keyboardForm.ShowDialog() == DialogResult.OK)
            {
                Note = keyboardForm.EnteredText;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
