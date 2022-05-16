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
        public byte[] GetIncomeFile(IEnumerable<ProductIncoms> data,string supplierName)
        {
            try
            {
                byte[] file = File.ReadAllBytes(TemplateConfig.IncomePath);

                ExcelPackage pck = new ExcelPackage();
                pck.Load(new MemoryStream(file));
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                int i = 10;
                string header = ws.Cells["A4"].Value.ToString() + data.FirstOrDefault().IncomeNumber;
                ws.Cells["A4"].Value = header;
                ws.Cells["A5"].Value = ws.Cells["A5"].Value.ToString() + DateTime.Now.ToString("dd.MM.yyyy");
                ws.Cells["A6"].Value = ws.Cells["A6"].Value.ToString() + supplierName;
                ws.InsertRow(i, data.Count(), 5);
                ws.Cells["A10"].LoadFromCollection(data.Select((x, i) => new
                {
                    Number = i+1,
                    Name = x.Product.Name,
                    UnitName = x.Product.Unit.Name,
                    Cost = x.IncomeCost,
                    Amount = x.Amount,
                    Sum = x.IncomeCost * x.Amount

                }), false);

                ws.Cells[10 + data.Count(), 5].Value = "Итого:";
                ws.Cells[10 + data.Count(), 6].Value=data.Sum(x=>(x.IncomeCost * x.Amount));

                return pck.GetAsByteArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] GetSaleFile(IEnumerable<ProductSales> data,string clientName)
        {
            try
            {
                byte[] file = File.ReadAllBytes(TemplateConfig.SalePath);

                ExcelPackage pck = new ExcelPackage();
                pck.Load(new MemoryStream(file));
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                int i = 10;
                string header = ws.Cells["A4"].Value.ToString() + data.FirstOrDefault().OrderNumber;
                ws.Cells["A4"].Value = header;
                ws.Cells["A5"].Value = ws.Cells["A5"].Value.ToString() + DateTime.Now.ToString("dd.MM.yyyy");
                ws.Cells["A6"].Value = ws.Cells["A6"].Value.ToString() + clientName;
                ws.InsertRow(i, data.Count(), 5);
                ws.Cells["A10"].LoadFromCollection(data.Select((x, i) => new
                {
                    Number = i + 1,
                    Name = x.Product.Name,
                    UnitName = x.Product.Unit.Name,
                    Cost = x.SaleCost,
                    Amount = x.Amount,
                    Sum = x.SaleCost * x.Amount

                }), false);

                ws.Cells[10 + data.Count(), 5].Value = "Итого:";
                ws.Cells[10 + data.Count(), 6].Value = data.Sum(x => (x.SaleCost * x.Amount));

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
