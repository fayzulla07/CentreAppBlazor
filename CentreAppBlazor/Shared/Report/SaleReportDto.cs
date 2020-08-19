using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
    public class SaleReportDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double SaleCost { get; set; }
        public double TotalSaleCost { get; set; }
        public string Client { get; set; }
        public string OptCost { get; set; }
        public DateTime RegDt { get; set; }
        public int OrderNumber { get; set; }
        public string Comments { get; set; }
        public bool IsBank { get; set; }
    }
}
