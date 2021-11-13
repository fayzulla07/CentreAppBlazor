using CentreAppBlazor.Shared.Report;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CentreAppBlazor.Server.ReportToExcel
{
    public class IncomeReport : ExcelReport, IReportToFile
    {

        public MemoryStream GetFile(string title, object items)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                IEnumerable<IncomeReportDto> itemlist = items as IEnumerable<IncomeReportDto>;
                if (itemlist == null) itemlist = new List<IncomeReportDto>();

                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");
                SetType(typeof(IncomeReportDto));
                PrepareTitle(1, title, ws);
                PrepareHead(2, ws);
                LoadData("A3", itemlist, ws);

                ws.Column(7).Style.Numberformat.Format = "dd-MM-yyyy";
                ws.Column(7).Width = 10;

                int iColCnt = ws.Dimension.End.Column;
                int iRowCnt = ws.Dimension.End.Row + 1;


                using (ExcelRange r = ws.Cells[iRowCnt, 1, iRowCnt, iColCnt])
                {
                    r.Merge = true;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    double itogo = 0;
                    double pribl = 0;
                    //double opt = 0;
                    foreach (var item in itemlist)
                    {
                        itogo += (item.IncomeCost * item.Amount);
                        pribl += (item.SaleCost * item.Amount) - (item.IncomeCost * item.Amount);
                        //opt += (item.OptCost * item.Amount) - (item.IncomeCost * item.Amount);
                    }
                     r.Value = $"Итого приход: {itogo.ToString("C0", new CultureInfo("kk-KZ"))} \r\n Розничная: {pribl.ToString("C0", new CultureInfo("kk-KZ"))}";
                }

                return new MemoryStream(pck.GetAsByteArray()); //Get updated stream
            }
        }
    }
}
