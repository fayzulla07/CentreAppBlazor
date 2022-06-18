using CentreAppBlazor.Shared.Domain;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CentreAppBlazor.Shared.Excel
{
    public class Helper
    {
        public List<ProductTypes> Read1CExcel(byte[] file,bool addIncomes, double kurs, decimal saleCostPercentAdd)
        {
            //try
            //{
            ExcelPackage pck = new ExcelPackage();
            pck.Load(new MemoryStream(file));
            ExcelWorksheet ws = pck.Workbook.Worksheets.First();
            string ProductName = string.Empty;
            List<string> names = new List<string>();
            List<ProductTypes> ptList = new List<ProductTypes>();
            ProductTypes pt;

            for (int i = 6; i < ws.Dimension.Rows - 2; i++)
            {
                pt = new ProductTypes();
                if (ws.Cells[i, 2].Value is null) continue;
                //get the B cell value 
                if (ws.Cells[i, 2].Style.Font.Bold)
                {
                    ProductName = ws.Cells[i, 2].Value.ToString();
                    pt.Name = ProductName;
                    ptList.Add(pt);
                    names.Add(ProductName);
                    //continue;
                }
                else
                {
                    Products p = new Products();
                    string code = ws.Cells[i, 2].Value.ToString();
                    p.Code = code;
                    string model = ws.Cells[i, 4].Value.ToString();
                    string color = ws.Cells[i, 6].Value.ToString();
                    string size = ws.Cells[i, 8].Value.ToString();
                    p.Name = $"{model} {color} {size}";
                    p.Description = $"{ProductName} {model} {color} {size}";
                    p.Limit = 1; p.UnitId = 1; p.RemainCount = 0;
                    double col = Convert.ToDouble(ws.Cells[i, 11].Value);
                    double kv = Convert.ToDouble(ws.Cells[i, 9].Value);
                    double sumKv = Convert.ToDouble(ws.Cells[i, 12].Value);
                    p.Volume = sumKv/col;
                    //pt.Products.Add(p);
                    ptList.Where(x => x.Name.Equals(ProductName)).Select(p => p.Products).First().Add(p);

                    // create the product incomes
                    if (addIncomes)
                    {
                        ProductIncoms pi = new ProductIncoms();

                        decimal cost = Convert.ToDecimal(ws.Cells[i, 13].Value);
                        decimal sumCost = Convert.ToDecimal(ws.Cells[i, 15].Value);

                        pi.Product = p;
                        pi.Kurs = kurs;
                        //pi.IncomeNumber = 0; // get the latest+1 will be added during insert to db
                        pi.IncomeCost = cost; // cost for kv
                        pi.SaleCost = (pi.IncomeCost * saleCostPercentAdd / 100)+pi.IncomeCost;
                        pi.OptCost = pi.SaleCost;
                        pi.Amount = col; // if you will be use the SP, otherwise use the sumKv variable
                        ptList.Where(x => x.Name.Equals(ProductName))
                              .Select(p => p.Products.Where(p => p.Code.Equals(code))).First()
                              .Select(f => f.ProductIncoms).First().Add(pi);
                    }
                }
            }
            return ptList;
           //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
