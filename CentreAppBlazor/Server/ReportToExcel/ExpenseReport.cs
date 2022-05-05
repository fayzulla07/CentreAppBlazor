using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Report;
using OfficeOpenXml;
namespace CentreAppBlazor.Server.ReportToExcel
{
    public class ExpenseReport : ExcelReport, IReportToFile
    {
        public MemoryStream GetFile(string title, object items)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                IEnumerable<Expenses> itemlist = items as List<Expenses>;
                if (itemlist == null) itemlist = new List<Expenses>();

                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");
                SetType(typeof(Expenses));
                PrepareTitle(1, title, ws);
                PrepareHead(2, ws);
                LoadData("A3", itemlist.Select(p=>new{ 
                    Расход=p.Name,
                    Цена=p.Cost,
                    ДатаВремя=p.RegDateTime.ToString()
                }), ws);

                ws.Cells["C3:C" + (itemlist.Count() + 2)].Style.Numberformat.Format = "dd-MM-yyyy HH:mm";
                ws.Column(3).Width = 14;
                ws.Cells["A2"].Value = "Расход";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["B2"].Value = "Цена";
                ws.Cells["B2"].Style.Font.Bold = true;
                ws.Cells["C2"].Value = "ДатаВремя";
                ws.Cells["C2"].Style.Font.Bold = true;
                int iColCnt = ws.Dimension.End.Column;
                int iRowCnt = ws.Dimension.End.Row + 2;


                using (ExcelRange r = ws.Cells[iRowCnt - 1, 1, iRowCnt - 1, iColCnt])
                {
                    r.Merge = true;
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    double total = 0;
                    double incomeTotal = 0;
                    foreach (var item in itemlist)
                    {
                        total += item.Cost;
                    }
                    r.Value = $"Итого Расходы: {total.ToString("C0", new CultureInfo("kk-KZ"))} \t ";
                }
                /// -------------------------------------------------------------------------------------------------------------- ///

                return new MemoryStream(pck.GetAsByteArray()); //Get updated stream
            }
        }
    }
}
