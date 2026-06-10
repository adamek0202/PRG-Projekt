using System;
using System.Runtime.InteropServices;

namespace Pokladna.Events
{
    public class RawPrinterHelper
    {
        // Použití struktury s explicitním Layoutem je pro Win32 API mnohem stabilnější
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.drv", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, ref DOCINFO di);

        [DllImport("winspool.drv", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName, byte[] bytes)
        {
            IntPtr hPrinter = new IntPtr(0);
            bool bSuccess = false;

            // Inicializace struktury přímo na stacku
            DOCINFO di = new DOCINFO();
            di.pDocName = $"Receipt_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
            di.pDataType = "RAW";

            // Sledování chyb pomocí Marshal.GetLastWin32Error()
            if (!OpenPrinter(szPrinterName, out hPrinter, IntPtr.Zero))
            {
                int err = Marshal.GetLastWin32Error();
                Serilog.Log.Error("OpenPrinter selhal s chybou: {ErrCode}", err);
                return false;
            }

            // Zde předáváme strukturu pomocí klíčového slova ref
            if (!StartDocPrinter(hPrinter, 1, ref di))
            {
                int err = Marshal.GetLastWin32Error();
                Serilog.Log.Error("StartDocPrinter selhal s chybou: {ErrCode}", err);
                ClosePrinter(hPrinter);
                return false;
            }

            if (!StartPagePrinter(hPrinter))
            {
                int err = Marshal.GetLastWin32Error();
                Serilog.Log.Error("StartPagePrinter selhal s chybou: {ErrCode}", err);
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
                return false;
            }

            // Alokace unmanaged paměti pro samotná data
            IntPtr pBytes = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, pBytes, bytes.Length);

            int dwWritten = 0;
            bSuccess = WritePrinter(hPrinter, pBytes, bytes.Length, out dwWritten);

            if (!bSuccess)
            {
                int err = Marshal.GetLastWin32Error();
                Serilog.Log.Error("WritePrinter selhal s chybou: {ErrCode}", err);
            }

            Marshal.FreeHGlobal(pBytes);
            EndPagePrinter(hPrinter);
            EndDocPrinter(hPrinter);
            ClosePrinter(hPrinter);

            return bSuccess;
        }
    }
}
