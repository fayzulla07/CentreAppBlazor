using CentreAppBlazor.Shared.Report;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace CentreAppBlazor.Server.ReportToExcel
{
    public class SalesReport : ExcelReport, IReportToFile
    {

        public MemoryStream GetFile(string title, object items)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                SaleReturnReportDto itemlist = items as SaleReturnReportDto;
                if (itemlist == null) itemlist = new SaleReturnReportDto();

                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");
                SetType(typeof(SaleReportDto));
                PrepareTitle(1, title, ws);
                PrepareHead(2, ws);
                LoadData("A3", itemlist.sale, ws);

                ws.Cells["F3:F" + (itemlist.sale.Count + 2)].Style.Numberformat.Format = "dd-MM-yyyy";
                ws.Column(6).Width = 14;

                int iColCnt = ws.Dimension.End.Column;
                int iRowCnt = ws.Dimension.End.Row + 2;


                using (ExcelRange r = ws.Cells[iRowCnt - 1, 1, iRowCnt - 1, iColCnt])
                {
                    r.Merge = true;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    double total = 0;
                    foreach (var item in itemlist.sale)
                    {
                        total += item.TotalSaleCost;
                     
                    }
                    r.Value = $"Итого: {total.ToString("C0", new CultureInfo("kk-KZ"))}";
                }
 /// -------------------------------------------------------------------------------------------------------------- ///
                SetType(typeof(ReturnReportDto));
                PrepareTitle(iRowCnt + 2, title.Replace("Продажа", "Возврат"), ws);
                PrepareHead(iRowCnt + 3, ws);
                LoadData("A" + (iRowCnt + 4), itemlist.returns, ws);

                ws.Cells["D" + (itemlist.sale.Count + 6) + ":D" + (itemlist.sale.Count + 6 + itemlist.returns.Count + 3) ].Style.Numberformat.Format = "dd-MM-yyyy";
                ws.Column(4).Width = 13;

                iColCnt = ws.Dimension.End.Column;
                 iRowCnt = ws.Dimension.End.Row + 1;

                using (ExcelRange r = ws.Cells[iRowCnt, 1, iRowCnt, 5])
                {
                    r.Merge = true;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    double total = 0;
                    foreach (var item in itemlist.returns)
                    {
                        total += item.ReturnCost;

                    }
                    r.Value = $"Итого: {total.ToString("C0", new CultureInfo("kk-KZ"))}";
                }

                return new MemoryStream(pck.GetAsByteArray()); //Get updated stream
            }
        }
    }
}
