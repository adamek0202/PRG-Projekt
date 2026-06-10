using Pokladna.Events;
using Pokladna.Forms.ProductSelectionForms;
using System;
using System.Windows.Forms;

namespace Pokladna.Forms.ManagementForms
{
    public partial class ManagerForm : BaseForm
    {
        private Ribbon wpfRibbon;
        // Konstruktor přijímá sdílený kontext z úvodní obrazovky
        internal ManagerForm(PosContext context) : base(context)
        {
            InitializeComponent();
            wpfRibbon = (Ribbon)elementHost1.Child;
        }

        private void OnRibbonActionTriggered(object sender, AppActionEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnRibbonActionTriggered(sender, e)));
                return;
            }

            switch (e.Action)
            {
                case AppActionType.OpenPriceList:
                    //OpenSingleWindow<PriceListForm>();
                    return;

                case AppActionType.OpenGiftCards:
                    OpenSingleWindow<GiftCardsForm>();
                    return;

                case AppActionType.CloseWindow:
                    // Tlačítko Konec z jakékoliv sekce zavře aktivní okno
                    if (ActiveMdiChild != null)
                    {
                        ActiveMdiChild.Close();
                        // Po zavření okna řekneme Ribbonu, že nic neběží -> vrátí se na záložku "Domů"
                        wpfRibbon.UpdateRibbonState(null);
                    }
                    return;
            }

            // Ostatní datové akce (Add, Edit, Delete) posíláme do běžícího okna
            if (ActiveMdiChild is IRibbonActionTarget activeWindow)
            {
                switch (e.Action)
                {
                    case AppActionType.AddRecord: activeWindow.ExecuteAdd(); break;
                }
            }
        }

        /// <summary>
        /// Otevře požadované okno a nahlásí to Ribbonu, aby přepnul záložku.
        /// </summary>
        private void OpenSingleWindow<T>() where T : Form, new()
        {
            // Protože může běžet jen jedno okno, pojistka:
            if (ActiveMdiChild != null) return;

            T newChild = new T();
            newChild.MdiParent = this;
            newChild.WindowState = FormWindowState.Maximized;

            // Navážeme se na událost FormClosed samotného okna. 
            // Kdyby uživatel okno zavřel jinak (třeba křížkem, pokud ho tam necháš), 
            // Ribbon se musí taky správně vrátit na domovskou záložku.
            newChild.FormClosed += (s, args) => { wpfRibbon.UpdateRibbonState(null); };

            newChild.Show();

            // Řekneme Ribbonu, jaké okno se zrovna otevřelo (např. "PriceListForm")
            wpfRibbon.UpdateRibbonState(typeof(T).Name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppEventBroker.ActionTriggered -= OnRibbonActionTriggered;
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}