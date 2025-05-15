using PCSC;
using PCSC.Monitoring;
using System;
using System.Data.SQLite;
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
            ReallyCenterToScreen(this);
            CheckForIllegalCrossThreadCalls = false;
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
            NfcReader.Instance.CardUidReceived += ReadCard;
        }

        private void GiftCardReadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NfcReader.Instance.CardUidReceived -= ReadCard;
        }

        private void ReadCard(string uid)
        {
            label1.Text = "Čtení karty...";
            if(uid.Length == 20)
            {
                const string querry = "SELECT * from GiftCards WHERE Code = @code";
                using (var command = new SQLiteCommand(querry, DatabaseConnection.Connection))
                {
                    command.Parameters.AddWithValue("code", uid);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //MessageBox.Show($"Karta s ID {uid}\nDržitele {reader["Holder"]}\nbyla nalezena");
                        }
                        else
                        {
                            //MessageBox.Show($"Karta s ID {uid} nebyla nalezena");
                        }
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            else {
                MessageBox.Show($"Karta s ID {uid} není platná dárková karta...", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
                return;
            }
        }
    }
}