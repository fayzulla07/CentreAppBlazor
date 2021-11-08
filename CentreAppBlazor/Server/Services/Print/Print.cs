using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Shared.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CentreAppBlazor.Server.Services
{ 
    public class Print
    {
        public byte[] GetIncomeFile(IEnumerable<ProductIncoms> data)
        {
            try
            {
                byte[] file = File.ReadAllBytes(TemplateConfig.IncomePath);

                ExcelPackage pck = new ExcelPackage();
                pck.Load(new MemoryStream(file));
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                int i = 5;
                string header = ws.Cells["A1"].Value.ToString() + data.FirstOrDefault().IncomeNumber;
                ws.Cells["A1"].Value = header;
                ws.Cells["A2"].Value = ws.Cells["A2"].Value.ToString() + DateTime.Now.ToString("MM.dd.yyyy");
                ws.InsertRow(i, data.Count(), 5);
                int rowCount = 0;
                ws.Cells["A5"].LoadFromCollection(data.Select(x => new
                {
                    Number = ++rowCount,
                    Name = x.Product.Name,
                    UnitName = x.Product.Unit.Name,
                    Cost = x.IncomeCost,
                    Amount = x.Amount,
                    Sum = x.IncomeCost * x.Amount

                }), false);

                ws.Cells[5 + data.Count(), 5].Value = "Итого:";
                ws.Cells[5 + data.Count(), 6].Value=data.Sum(x=>(x.IncomeCost * x.Amount));

                return pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] GetSaleFile(IEnumerable<ProductSales> data)
        {
            try
            {
                byte[] file = File.ReadAllBytes(TemplateConfig.SalePath);

                ExcelPackage pck = new ExcelPackage();
                pck.Load(new MemoryStream(file));
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                int i = 5;
                string header = ws.Cells["A1"].Value.ToString() + data.FirstOrDefault().OrderNumber;
                ws.Cells["A1"].Value = header;
                ws.Cells["A2"].Value = ws.Cells["A2"].Value.ToString() + DateTime.Now.ToString("MM.dd.yyyy");
                ws.InsertRow(i, data.Count(), 5);
                int rowCount = 0;
                ws.Cells["A5"].LoadFromCollection(data.Select(x => new
                {
                    Number = ++rowCount,
                    Name = x.Product.Name,
                    UnitName = x.Product.Unit.Name,
                    Cost = x.SaleCost,
                    Amount = x.Amount,
                    Sum = x.SaleCost * x.Amount

                }), false);

                ws.Cells[5 + data.Count(), 5].Value = "Итого:";
                ws.Cells[5 + data.Count(), 6].Value = data.Sum(x => (x.SaleCost * x.Amount));

                return pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
