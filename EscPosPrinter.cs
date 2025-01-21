using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    internal class UsbEscPosPrinter
    {
        private string printerDevicePath;
        private const int MaxCharsPerLine = 32;

        public UsbEscPosPrinter(string devicePath)
        {
            printerDevicePath = devicePath;
        }

        public void Print(byte[] data)
        {
            try
            {
                using (var fileStream = new FileStream(printerDevicePath, FileMode.Open, FileAccess.Write))
                {
                    fileStream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při tisku: {ex.Message}");
            }
        }

        public void PrintText(string text, bool bold = false, bool underline = false, string alignment = "left")
        {
            var builder = new StringBuilder();

            // Zarovnání textu
            switch (alignment.ToLower())
            {
                case "center":
                    builder.Append(CenterAlign(text));
                    break;
                case "right":
                    builder.Append(RightAlign(text));
                    break;
                default:
                    builder.Append(text); // Výchozí zarovnání vlevo
                    break;
            }

            // Tučné písmo
            if (bold)
                builder.Insert(0, "\x1B\x45\x01"); // ESC E - Bold zapnout
            if (underline)
                builder.Insert(0, "\x1B\x2D\x01"); // ESC - - Podtržení zapnout

            // Vypnutí stylování
            if (bold)
                builder.Append("\x1B\x45\x00"); // ESC E - Bold vypnout
            if (underline)
                builder.Append("\x1B\x2D\x00"); // ESC - - Podtržení vypnout

            builder.Append("\n"); // Nový řádek

            Print(Encoding.UTF8.GetBytes(builder.ToString()));
        }

        private string CenterAlign(string text)
        {
            int padding = (MaxCharsPerLine - text.Length) / 2;
            if (padding > 0)
                return new string(' ', padding) + text;
            return text;
        }

        private string RightAlign(string text)
        {
            int padding = MaxCharsPerLine - text.Length;
            if (padding > 0)
                return new string(' ', padding) + text;
            return text;
        }
    }
}
