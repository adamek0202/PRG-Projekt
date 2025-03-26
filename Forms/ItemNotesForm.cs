using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class ItemNotesForm : Form
    {
        public string Note { get; private set; }

        private string[] Notes = {"Extra majoneza", "Bez rajcat", "Bez majonezy", "Extra sul", "Bez salatu", "Extra omacka", "Bez soli", "Bez omacky"};

        public ItemNotesForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
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
