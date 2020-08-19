using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Report
{
    public class IncomeReportDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public double IncomeCost { get; set; }
        public double SaleCost { get; set; }
        public double OptCost { get; set; }
        public DateTime? RegDt { get; set; }
        public string UserName { get; set; }
        public double Amount { get; set; }
    }
}
