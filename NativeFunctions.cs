using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//Implementace nativních funkcí WinAPI
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Projekt
{
    internal class NativeFunctions
    {
        // Konstanty pro specifikaci stylů a scrollbarů
        public const int GWL_STYLE = -16;
        public const int WS_VSCROLL = 0x00200000; // Styl pro vertikální scrollbar
        public const int WS_HSCROLL = 0x00100000; // Styl pro horizontální scrollbar
        public const int SB_VERT = 1; // Vertikální scrollbar

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        // Import WinAPI funkce SetWindowLong a GetWindowLong
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // Import WinAPI funkce ShowScrollBar
        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        public static void DisableVisualStyles(Control control)
        {
            SetWindowTheme(control.Handle, "", "");
        }
    }
}
