using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
   public class ReturnReportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ReturnCost { get; set; }
        public int Amount { get; set; }
        public DateTime RegDt { get; set; }
        public string CustomerName { get; set; }
    }
}
