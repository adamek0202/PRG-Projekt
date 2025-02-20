using Projekt.Forms;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Projekt
{
    internal static class PDFGeneration
    {
        public static MemoryStream GenerateTransactionsPdf(List<Sale> sales)
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
                        var chunks = sales.Chunk(47).ToList();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            var chunk = chunks[i];

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(colums =>
                                {
                                    colums.ConstantColumn(40); //Číslo účtu
                                    colums.ConstantColumn(90); //Datum
                                    colums.ConstantColumn(50); //Čas
                                    colums.ConstantColumn(110); //Uživtel
                                    colums.ConstantColumn(100); //Platba
                                    colums.ConstantColumn(80); //Cena

                                });

                                table.Header(header =>
                                {
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Číslo").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Datum").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Čas").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Uživatel").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Platba").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Cena").Bold().AlignRight();
                                });

                                foreach (var sale in chunk)
                                {
                                    table.Cell().Text(sale.Number.ToString());
                                    table.Cell().Text(sale.DateAndTime.Date.ToShortDateString());
                                    table.Cell().Text($"{sale.DateAndTime.Hour}:{sale.DateAndTime.Minute}");
                                    table.Cell().Text(sale.User);
                                    table.Cell().Text(sale.Payment);
                                    table.Cell().Text(sale.Price.ToString() + " Kč").AlignRight();
                                }
                            });
                            if (i < chunks.Count - 1)
                            {
                                col.Item().PageBreak();
                            }
                        }
                    });

                    page.Footer()
                        .Column(footer =>
                        {
                            footer.Item().AlignLeft().PaddingBottom(5).ShowOnce().Row(row =>
                            {
                                row.ConstantItem(410).BorderTop(3).Text("Suma");
                                row.ConstantItem(50).BorderTop(3).Text(SumTransactionsPrice(sales) + " Kč");
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

        public static MemoryStream GenerateProductsPdf(List<ItemSalesForm.Product> products)
        {
            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Padding(5).Text("Výpis prodeje produktů").Bold().FontSize(20);

                    page.Content().Column(col =>
                    {
                        var chunks = products.Chunk(47).ToList();
                        for (int i = 0; i < chunks.Count; i++)
                        {
                            var chunk = chunks[i];

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(colums =>
                                {
                                    colums.ConstantColumn(40); //Kód
                                    colums.ConstantColumn(150); //Název
                                    colums.ConstantColumn(50); //Cena Ks
                                    colums.ConstantColumn(35); //Kusů
                                    colums.ConstantColumn(65); //Celkem
                                });

                                table.Header(header =>
                                {
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Kód").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Název").Bold();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Cena Ks").Bold().AlignRight();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Kusů").Bold().AlignRight();
                                    header.Cell().PaddingBottom(3).BorderBottom(3).Text("Celkem").Bold().AlignRight();
                                });

                                foreach(var item in products)
                                {
                                    table.Cell().Text(item.Id.ToString());
                                    table.Cell().Text(item.Name);
                                    table.Cell().Text($"{item.Price.ToString()} Kč").AlignRight();
                                    table.Cell().Text(item.Count.ToString()).AlignRight();
                                    table.Cell().Text($"{(item.Count * item.Price).ToString()} Kč").AlignRight();
                                }
                            });

                            if (i < chunks.Count - 1)
                            {
                                col.Item().PageBreak();
                            }
                        }
                    });

                    page.Footer()
                        .Column(footer =>
                        {
                            footer.Item().AlignLeft().PaddingBottom(5).ShowOnce().Row(row =>
                            {
                                row.ConstantItem(410).BorderTop(3).Text("Suma");
                                row.ConstantItem(50).BorderTop(3).Text(SumProductsPrice(products) + " Kč");
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

        private static string SumTransactionsPrice(List<Sale> sales)
        {
            int price = 0;
            foreach (var item in sales)
            {
                price += item.Price;
            }
            return price.ToString();
        }

        private static string SumProductsPrice(List<ItemSalesForm.Product> items)
        {
            int price = 0;
            foreach(var item in items)
            {
                price += item.Price * item.Count;
            }
            return price.ToString();
        }
    }

    internal static class ListExtensions
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
