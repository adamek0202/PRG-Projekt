using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Pokladna
{
    internal class ListRow : ListViewItem
    {
        public ListRow()
        {
        }

        public ListRow(string[] items) : base(items)
        {
        }

        public ListRow(string[] items, ListViewGroup group) : base(items, group)
        {
        }

        public ListRow(string text, ListViewGroup group) : base(text, group)
        {
        }

        public string CouponID { get; set; } = string.Empty;
    }
}
