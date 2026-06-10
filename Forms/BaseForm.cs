             using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pokladna.Forms
{
    public class BaseForm : Form
    {
        // Každý formulář, který z tohoto zdědí, bude mít okamžitě přístup k _context
        internal PosContext _context {  get; private set; } 

        // Prázdný konstruktor pro Visual Studio Designer (Designer neumí pracovat s parametry)
        public BaseForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Konstruktor, který budeme reálně používat v kódu
        internal BaseForm(PosContext context) : this()
        {
            _context = context;
        }

        // Automatické vypnutí DWM vykreslování okna, které jsi řešil ručně v PaymentForm
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            try
            {
                int renderingPolicy = 1; // DWMNCRP_DISABLED
                DwmSetWindowAttribute(Handle, 2, ref renderingPolicy, sizeof(int));
            }
            catch (Exception ex)
            {
                Serilog.Log.Warning(ex, "Nepodařilo se aplikovat DWM politiku pro okno {FormName}", Name);
            }
        }

        // P/Invoke deklarace zabudovaná přímo v základu
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
    }
}