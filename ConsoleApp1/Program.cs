using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SomeClass sm = new SomeClass();
            sm.ToDo2();

            string[] students = new string[10];
            //Init
            Console.WriteLine("Initilization=>");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("Enter student " + (i + 1) + "=");
                students[i] = Console.ReadLine();
            }
            Console.WriteLine();
            // search value
            string filterValue;
            Console.Write("enter value for search:");
            filterValue = Console.ReadLine();
            Console.WriteLine();

            // search

            foreach (var item in students)
            {
                if(item==filterValue)
                    Console.WriteLine("found:"+item);
                //else
                //    Console.WriteLine("not found");
            }



            Console.ReadKey();
        }


        string[] Init()
        {
            string[] tempStudent = new string[10];

            for (int i = 0; i < 10; i++)
            {
                Console.Write("Enter student "+(i+1)+"=");
                tempStudent[i] = Console.ReadLine();
                //Console.WriteLine();
            }
            return null;
        }

        #region temp
        void RUn()
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //byte[] file = File.ReadAllBytes("C:\\temp\\test.xlsx");
            //ReadExcel();
        }

        public static void ReadExcel(byte[] file)
        {
            ExcelPackage pck = new ExcelPackage();
            pck.Load(new MemoryStream(file));
            ExcelWorksheet ws = pck.Workbook.Worksheets.First();
            string ProductName = string.Empty;
            for (int i = 6; i < ws.Dimension.Rows-2; i++)
            {
                if (ws.Cells[i, 2].Value is null) continue;
                //get the B cell value 
                if (ws.Cells[i, 2].Style.Font.Bold)
                {
                    ProductName = ws.Cells[i, 2].Value.ToString();
                    continue;
                }
                else
                {
                    string code = ws.Cells[i, 2].Value.ToString();
                    string model = ws.Cells[i, 4].Value.ToString();
                    string color = ws.Cells[i, 6].Value.ToString();
                    string size = ws.Cells[i, 8].Value.ToString();
                    string kv = ws.Cells[i, 9].Value.ToString();
                    string col = ws.Cells[i, 11].Value.ToString();
                    string sumKv = ws.Cells[i, 12].Value.ToString();
                    string cost = ws.Cells[i, 13].Value.ToString();
                    string sumCost = ws.Cells[i, 15].Value.ToString();
                    Console.WriteLine($"{i}=>{ProductName}_{code}_{model}_{color}_{size}={kv}_{col}_{sumKv}_{cost}_{sumCost}");
                }
                
            }
            //string ProductName = ws.Cells[i, 2].Value.ToString();
            int a = ws.Dimension.Rows;
            string header = ws.Cells["A4"].Value.ToString();
            ws.Cells["A4"].Value = header;
            ws.Cells["A5"].Value = ws.Cells["A5"].Value.ToString() + DateTime.Now.ToString("dd.MM.yyyy");

        }
        static DataTable ReadExcel()
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream("C:\\temp\\test.xls", FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString());
                            }
                        }
                    }
                    if (rowList.Count > 0)
                        dtTable.Rows.Add(rowList.ToArray());
                    rowList.Clear();
                }
            }
            return dtTable;
        }
        #endregion
    }

    class SomeClass
    {
        private void ToDo()
        {
            Console.WriteLine("to do");
        }
        public void ToDo2()
        {
            ToDo();
            Console.WriteLine("to do");
        }
    }
}
