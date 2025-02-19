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
            MemoryStream pdfStream = GeneratePdf(Sales);
            LoadPdfFromStream(pdfStream);
        }

        private void LoadPdfFromStream(MemoryStream stream)
        {
            byte[] pdfBytes = stream.ToArray();

            pdfDocument?.Dispose();
            pdfDocument = PdfDocument.Load(new MemoryStream(pdfBytes));
            pdfViewer1.Document = pdfDocument;
        }

        private MemoryStream GeneratePdf(List<Sale> sales)
        {
            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Padding(5).Text("Výpis transakcí").Bold().FontSize(20);

                    page.Content().Column(col =>
                    {
                        var chunks = Sales.Chunk(47).ToList();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            var chunk = chunks[i];

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(colums =>
                                {
                                    colums.ConstantColumn(90); //Datum
                                    colums.ConstantColumn(110); //Cena
                                    colums.ConstantColumn(60); //Platba
                                    colums.ConstantColumn(70); //Uživatel

                                });

                                table.Header(header =>
                                {
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Datum").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Uživatel").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Platba").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Cena").Bold().AlignRight();
                                });

                                foreach (var sale in chunk)
                                {
                                    table.Cell().Text(sale.Date);
                                    table.Cell().Text(sale.User);
                                    table.Cell().Text(sale.Payment);
                                    table.Cell().Text(sale.Price.ToString() + " Kč").AlignRight();
                                }
                            });
                            if (i < chunks.Count - 1)
                            {
                                col.Item().PageBreak();
                            }
                            else
                            {
                            }
                        }

                    });

                    page.Footer()
                        .Column(footer =>
                        {
                            footer.Item().AlignLeft().PaddingBottom(5).ShowOnce().Row(row =>
                            {
                                row.ConstantItem(300).BorderTop(3).Text("Suma");
                                row.ConstantItem(50).BorderTop(3).Text(SumPrice() + " Kč");
                            });

                            footer.Item().AlignCenter().Text(text =>
                            {
                                text.Span("Strana ");
                                text.CurrentPageNumber();
                                text.Span(" z ");
                                text.TotalPages();
                            });
                        });
                });
            });

            document.GeneratePdf(stream);

            stream.Position = 0;
            return stream;
        }

        private string SumPrice()
        {
            int price = 0;
            foreach(var item in Sales)
            {
                price += item.Price;
            }
            return price.ToString();
        }
    }

    public static class ListExtensions
    {
        public static IEnumerable<List<T>> Chunk<T>(this List<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count; i += chunkSize)
            {
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
            }
        }
    }
}
