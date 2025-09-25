using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokladna.Forms.ManagementForms
{
    internal partial class CouponEditForm : Form
    {
        public Coupon Coupon { get; private set; }
        public CouponEditForm(Coupon editedCoupon)
        {
            this.Coupon = editedCoupon;
            InitializeComponent();
        }
    }
}
