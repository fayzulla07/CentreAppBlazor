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
                    Console.WriteLine(ProductName);
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
                    string kv = ws.Cells[i, 9].Value.ToString();
                    p.Volume = Convert.ToDouble(kv);
                    //pt.Products.Add(p);
                    ptList.Where(x => x.Name.Equals(ProductName)).Select(p => p.Products).First().Add(p);
                    
                    //p.ProductTypeId = 1; // get from pt or look at the ef core functions

                    // create the product incomes
                    if (addIncomes)
                    {
                        ProductIncoms pi = new ProductIncoms();
                        
                        string col = ws.Cells[i, 11].Value.ToString();
                        string sumKv = ws.Cells[i, 12].Value.ToString();
                        string cost = ws.Cells[i, 13].Value.ToString();
                        string sumCost = ws.Cells[i, 15].Value.ToString();

                        pi.Product = p;
                        pi.Kurs = kurs;
                        //pi.IncomeNumber = 0; // get the latest+1 will be added during insert to db
                        pi.IncomeCost = Convert.ToDecimal(cost); // cost for kv
                        pi.SaleCost = pi.IncomeCost * saleCostPercentAdd / 100; // get from param
                        pi.OptCost = pi.SaleCost;
                        ptList.Where(x => x.Name.Equals(ProductName))
                            .Select(p => p.Products.Where(p => p.Code.Equals(code))).First()
                            .Select(f => f.ProductIncoms).First().Add(pi);
                    }
                    //pt.Products.Add(p); //add to pt
                    //ptList.Where(x => x.Name.Equals(ProductName)).Select(p => p.Products).First().Add(p);
                }
            }
            foreach (var item in ptList)
            {
                Console.WriteLine($"{item.Name}-{item.Products.Count()}");
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
