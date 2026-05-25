using Pokladna.Database;
using System;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    internal partial class GiftCardReadForm : BaseForm
    {
        // Vlastnost, kam si uložíme nalezenou kartu, aby si ji mohl MainForm po úspěchu vytáhnout
        public GiftCard ScannedGiftCard { get; private set; }

        internal GiftCardReadForm(PosContext context) : base(context)
        {
            InitializeComponent();
        }

        private void GiftCardReadForm_Load(object sender, EventArgs e)
        {
            // Přihlášení k nfc události singeltonu
            NfcReader.Instance.CardUidReceived += OnCardUidReceived;
        }

        private void GiftCardReadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Striktní odhlášení, aby nedocházelo k únikům paměti (Memory Leaks)
            NfcReader.Instance.CardUidReceived -= OnCardUidReceived;
        }

        /// <summary>
        /// Událost odpálená z background vlákna PCSC knihovny
        /// </summary>
        private void OnCardUidReceived(string uid)
        {
            // !!! KLÍČOVÉ ROZHRANÍ PRO VLÁKNA !!!
            // Pokud kód běží na jiném vlákně než okno, přepošleme ho bezpečně do UI vlákna
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnCardUidReceived), uid);
                return;
            }

            // --- TADY UŽ JSME STOPROCENTNĚ V BEZPEČNÉM UI VLÁKNĚ ---
            label1.Text = "Čtení karty...";

            // UID z PCSC vrací hex řetězec oddělený pomlčkami (např. "04-A2-B3-C4...") 
            // Očistíme délku, tvůj starý kód kontroloval natvrdo délku 20 znaků
            if (string.IsNullOrWhiteSpace(uid))
            {
                MessageBox.Show("Nepodařilo se přečíst kód karty.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Zavoláme databázovou vrstvu (MySQL) mimo GUI
            GiftCard card = DatabaseFunctions.GetGiftCardByUid(uid);

            if (card != null)
            {
                // Kartu si schováme do vlastnosti a zavřeme okno s výsledkem OK
                ScannedGiftCard = card;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show($"Karta s ID {uid} nebyla v databázi nalezena.", "Karta neexistuje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}