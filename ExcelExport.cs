using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Pokladna
{
    internal static class ExcelExport
    {
        public static void Export(DataTable table ,string path) {
            if(table == null || table.Columns.Count == 0)
            {
                throw new ArgumentException();
            }
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Export");
                var tableRange = ws.Cell(1, 1).InsertTable(table, true);
                var rng = tableRange.RangeUsed();

                rng.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                rng.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                var header = rng.Range(1, 1, 1, table.Columns.Count);
                header.Style.Border.BottomBorder = XLBorderStyleValues.Thick;

                ws.Columns().AdjustToContents();
                wb.SaveAs(path);
            }
        }
    }
}
