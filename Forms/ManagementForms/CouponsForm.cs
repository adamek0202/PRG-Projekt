using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace Pokladna.Forms.ManagementForms
{
    public partial class CouponsForm : Form
    {
        public CouponsForm()
        {
            InitializeComponent();
            NativeFunctions.DisableVisualStyles(listViewWithScrollBar1);
        }

        private List<CommandBinding> _bindings = new();

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if(_bindings.Count == 0)
            {
                _bindings.Add(new CommandBinding(RibbonCommands.Add, (s, args) => AddCoupon()));
                _bindings.Add(new CommandBinding(RibbonCommands.Edit, (s, args) => EditCoupon()));
                _bindings.Add(new CommandBinding(RibbonCommands.Delete, (s, args) => RemoveCoupon()));
            }
        }

        private void listViewWithScrollBar1_DoubleClick(object sender, System.EventArgs e)
        {
            EditCoupon();
        }

        private void AddCoupon() { }
        private void EditCoupon() {
            if(listViewWithScrollBar1.SelectedItems.Count == 1)
            {
                var cef = new CouponEditForm(new Coupon());
                if(cef.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        private void RemoveCoupon() {
            
        }
    }
}
