using Pokladna.Forms.ProductSelectionForms;

namespace Pokladna.Forms
{
    // Dědí z BaseForm, odebíráme ruční Win32 API pro DWM a centrování
    public partial class ManagerForm : BaseForm
    {
        // Konstruktor přijímá sdílený kontext z úvodní obrazovky
        internal ManagerForm(PosContext context) : base(context)
        {
            InitializeComponent();

            // Inicializace WPF Ribbonu uvnitř ElementHostu
            elementHost1.Child = new Ribbon();

            // !!! OPRAVA: Metodu musíme reálně zavolat, aby Ribbon reagoval na klikání !!!
            InitRibbonCommands();
        }

        private void InitRibbonCommands()
        {
            var ribbon = (Ribbon)elementHost1.Child;

            ribbon.CommandInvoked += (s, cmd) =>
            {
                switch (cmd.Name)
                {
                    case nameof(RibbonCommands.PriceList):
                        // Tady pak otevřeš ceníky
                        break;

                    case nameof(RibbonCommands.Employees):
                        // Tady správu zaměstnanců
                        break;

                    case nameof(RibbonCommands.GiftCards):
                        // Tady dárkové karty
                        break;

                    case nameof(RibbonCommands.Coupons):
                        // Otevře formulář kupónů jako MDI dítě a předá mu kontext
                        // (Až budeš CouponsForm upravovat, nezapomeň mu přidat konstruktor pro PosContext)
                        //var coupons = new CouponsForm(_context) { MdiParent = this };
                        //coupons.Show();
                        break;
                }
            };
        }
    }
}