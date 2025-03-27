using PdfiumViewer;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Pokladna.BasicTheme;

namespace Projekt.Forms
{
    internal partial class PrintPreviewForm : Form
    {
        private MemoryStream Stream;
        private PdfDocument pdfDocument;
        
        public PrintPreviewForm(MemoryStream stream)
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            Stream = stream;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DWMNCRENDERINGPOLICY renderingPolicy = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            int hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void PrintPreviewForm_Load(object sender, EventArgs e)
        {
            LoadPdfFromStream(Stream);
        }

        private void LoadPdfFromStream(MemoryStream stream)
        {
            byte[] pdfBytes = stream.ToArray();

            pdfDocument?.Dispose();
            pdfDocument = PdfDocument.Load(new MemoryStream(pdfBytes));
            pdfViewer1.Document = pdfDocument;
        }
    }
}
