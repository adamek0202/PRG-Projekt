using System.Windows.Forms;

namespace Pokladna.Forms.ManagementForms
{
    public partial class CouponsForm : Form
    {
        public CouponsForm()
        {
            InitializeComponent();
            NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);
        }

        private void listViewWithScrollBar1_DoubleClick(object sender, System.EventArgs e)
        {

        }
    }
}
