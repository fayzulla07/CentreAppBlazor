using CentreAppBlazor.Shared.Report;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CentreAppBlazor.Server.ReportToExcel
{
    public class RemainReport : ExcelReport, IReportToFile
    {

        public MemoryStream GetFile(string title, object items)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                IEnumerable<RemainReportDto> itemlist = items as IEnumerable<RemainReportDto>;
                if (itemlist == null) itemlist = new List<RemainReportDto>();

                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");
                SetType(typeof(RemainReportDto));
                PrepareTitle(1, title, ws);
                PrepareHead(2, ws);
                LoadData("A3", itemlist, ws);

                int iColCnt = ws.Dimension.End.Column;
                int iRowCnt = ws.Dimension.End.Row + 1;


                using (ExcelRange r = ws.Cells[iRowCnt, 1, iRowCnt, iColCnt])
                {
                    r.Merge = true;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    double saletotal = 0;
                    double remaintotal = 0;
                    foreach (var item in itemlist)
                    {
                        saletotal += item.SaleCost.Value;
                        remaintotal += item.RemainCount;
                    }
                    r.Value = $"Остаток: {remaintotal} шт";
                }


                return new MemoryStream(pck.GetAsByteArray()); //Get updated stream
            }
        }
    }
}
