using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Excel
{
    public class ProductTypeVM
    {
        public string Name { get; set; }
    }
    public class ProductsVM
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double RemainCount { get; set; }
        public int Limit { get; set; }
        public double? Volume { get; set; }
        public double? Amount { get; set; }
    }
}
