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
        private List<Sale> Sales;

        private PdfDocument pdfDocument;
        
        public PrintPreviewForm(List<Sale> sales)
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
            Sales = sales;
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
            MemoryStream pdfStream = PDFGeneration.GenerateTransactionsPdf(Sales);
            LoadPdfFromStream(pdfStream);
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
