using CentreAppBlazor.Server.Extensions;
using CentreAppBlazor.Shared.Attributes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentreAppBlazor.Server.ReportToExcel
{
    public abstract class ExcelReport
    {
        private Dictionary<string, string> properties;
        public ExcelReport()
        {
           
        }

        protected void PrepareTitle(int row, string title, ExcelWorksheet excelWorksheet) // A1
        {
            CheckDict();
            excelWorksheet.Cells["A" + row].Value = title;
            using (ExcelRange r = excelWorksheet.Cells[row, 1, row, properties.Count])
            {
                r.AutoFitColumns();
                r.Merge = true;
                r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
            }
        }
        protected void PrepareHead(int headrow, ExcelWorksheet excelWorksheet) // 2
        {
            CheckDict();
            int i = 0;
            foreach (var item in properties)
            {
                excelWorksheet.Cells[headrow, i + 1].Value = item.Value;
                i++;
            }
        }

        protected void LoadData<TItems>(string cells, IEnumerable<TItems> values, ExcelWorksheet excelWorksheet) // A3
        {
            CheckDict();
            //populate our Data
            if (values.Count() > 0)
            {
                excelWorksheet.Cells[cells].LoadFromCollectionFiltered(values);

            }
        }

        public void SetType(Type type)
        {
            properties = GetEpplusDisplay.Get(type);
        }
        private void CheckDict()
        {
            if (properties == null) throw new NullReferenceException("Вы должны присвоить SetType");
        }
    }
}
