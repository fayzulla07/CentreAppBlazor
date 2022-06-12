using CentreAppBlazor.Shared.Domain;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Services.Main
{
    public class MainService
    {
        public List<ProductTypes> Read1CExcel(byte[] file)
        {
            //try
            //{
                ExcelPackage pck = new ExcelPackage();
                pck.Load(new MemoryStream(file));
                ExcelWorksheet ws = pck.Workbook.Worksheets.First();
                string ProductName = string.Empty;
                List<ProductTypes> ptList = new List<ProductTypes>();
                ProductTypes pt = new ProductTypes();
                for (int i = 6; i < ws.Dimension.Rows - 2; i++)
                {
                    if (ws.Cells[i, 2].Value is null) continue;
                    //get the B cell value 
                    if (ws.Cells[i, 2].Style.Font.Bold)
                    {
                        ProductName = ws.Cells[i, 2].Value.ToString();
                        pt.Name = ProductName;
                        continue;
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
                        p.Description= $"{ProductName} {model} {color} {size}";
                        p.Limit = 1; p.UnitId = 1; p.RemainCount = 0;
                        string kv = ws.Cells[i, 9].Value.ToString();
                        p.Volume = Convert.ToDouble(kv);
                        p.ProductTypeId = 1; // get from pt or look at the ef core functions

                        // create the product incomes
                        if (true)
                        {
                            ProductIncoms pi = new ProductIncoms();

                            string col = ws.Cells[i, 11].Value.ToString();
                            string sumKv = ws.Cells[i, 12].Value.ToString();
                            string cost = ws.Cells[i, 13].Value.ToString();
                            string sumCost = ws.Cells[i, 15].Value.ToString();

                            pi.Product = p;
                            pi.Kurs = 1;// need to get as param
                            pi.IncomeNumber = 0; // get the latest+1
                            pi.IncomeCost = Convert.ToDecimal(cost); // cost for kv
                            pi.OptCost = pi.IncomeCost * 8 / 100; // also get from param
                            pi.SaleCost = pi.IncomeCost * 10 / 100; // get from param

                            p.ProductIncoms.Add(pi);
                        }
                        pt.Products.Add(p); //add to pt

                        //Console.WriteLine($"{i}=>{ProductName}_{code}_{model}_{color}_{size}={kv}_{col}_{sumKv}_{cost}_{sumCost}");
                    }
                    ptList.Add(pt);
                }
                return ptList;
                //string ProductName = ws.Cells[i, 2].Value.ToString();
                //int a = ws.Dimension.Rows;
                //string header = ws.Cells["A4"].Value.ToString();
                //ws.Cells["A4"].Value = header;
                //ws.Cells["A5"].Value = ws.Cells["A5"].Value.ToString() + DateTime.Now.ToString("dd.MM.yyyy");
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
