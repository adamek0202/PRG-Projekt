using PCSC;
using PCSC.Monitoring;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class GiftCardReadForm : Form
    {

        public GiftCardReadForm()
        {
            InitializeComponent();
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

        private void GiftCardReadForm_Load(object sender, EventArgs e)
        {
            NfcReader.Instance.CardUidReceived += uid =>
            {
                MessageBox.Show($"ID karty: {uid}");
            };
        }
    }
}