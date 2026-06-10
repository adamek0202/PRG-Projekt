using PdfiumViewer;
using System;
using System.IO;

namespace Pokladna.Forms
{
    internal partial class PrintPreviewForm : BaseForm
    {
        private MemoryStream Stream;
        private PdfDocument pdfDocument;
        
        public PrintPreviewForm(MemoryStream stream)
        {
            InitializeComponent();
            Stream = stream;
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
