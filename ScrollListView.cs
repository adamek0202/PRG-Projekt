using System;
using System.Windows.Forms;

//Neprovádět bezdůvodné zásahy do logiky, hrozí rozbití aplikace

namespace Projekt
{
    public class ListViewWithScrollBar : ListView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Přidání stylů scrollbarů
            int style = NativeFunctions.GetWindowLong(this.Handle, NativeFunctions.GWL_STYLE);
            NativeFunctions.SetWindowLong(this.Handle, NativeFunctions.GWL_STYLE, style | NativeFunctions.WS_VSCROLL);
            // Vynucení zobrazení scrollbar
            NativeFunctions.ShowScrollBar(this.Handle, NativeFunctions.SB_VERT, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // Zajisti, že scrollbar zůstane viditelný
            NativeFunctions.ShowScrollBar(this.Handle, NativeFunctions.SB_VERT, true);
        }
    }
}