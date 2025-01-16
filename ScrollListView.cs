using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;

//Neprovádět bezdůvodné zásahy do logiky, hrozí rozbití aplikace

namespace Projekt
{
    public class ListViewWithScrollBar : ListView
    {
        // Import WinAPI funkce SetWindowLong a GetWindowLong
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Import WinAPI funkce ShowScrollBar
        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        // Konstanty pro specifikaci stylů a scrollbarů
        private const int GWL_STYLE = -16;
        private const int WS_VSCROLL = 0x00200000; // Styl pro vertikální scrollbar
        private const int WS_HSCROLL = 0x00100000; // Styl pro horizontální scrollbar
        private const int SB_VERT = 1; // Vertikální scrollbar

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Přidání stylů scrollbarů
            int style = GetWindowLong(this.Handle, GWL_STYLE);
            SetWindowLong(this.Handle, GWL_STYLE, style | WS_VSCROLL);

            // Vynucení zobrazení scrollbarů
            ShowScrollBar(this.Handle, SB_VERT, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Zajisti, že scrollbar zůstane viditelný
            ShowScrollBar(this.Handle, SB_VERT, true);
        }
    }
}