using PCSC;
using PCSC.Monitoring;
using Sydesoft.NfcDevice;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Pokladna.Forms
{
    public partial class GiftCardReadForm : Form
    {
        private static ACR122U acr122u = new ACR122U();

        public GiftCardReadForm()
        {
            InitializeComponent();

            acr122u.Init(false, 50, 4, 4, 200);
            acr122u.CardInserted += ReadCard;
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

        private static void ReadCard(ICardReader reader)
        {
            MessageBox.Show($"ID karty: {BitConverter.ToString(acr122u.GetUID(reader))}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}