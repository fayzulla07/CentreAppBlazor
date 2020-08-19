using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
   public class RemainReportDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public double? IncomeCost { get; set; }
        public double? OptCost { get; set; }
        public double? SaleCost { get; set; }
        public double RemainCount { get; set; }
        public int Limit { get; set; }
       
    }
}
