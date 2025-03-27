using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class KeyboardForm : Form
    {
        public string EnteredText { get; private set; }

        public KeyboardForm()
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
            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
        }
    }
}
