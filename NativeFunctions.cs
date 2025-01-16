using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//Implementace nativních funkcí WinAPI
//Neprovádět bezdůvodné zásahy, hrozí rozbití aplikace

namespace Projekt
{
    internal class NativeFunctions
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hWnd, string appName, string partList);

        public static void DisableVisualStyles(Control control)
        {
            SetWindowTheme(control.Handle, "", "");
        }
    }
}
