using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class HistorySaleView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string AmountUnit { get; set; }
        public double SaleCost { get; set; }
        public double IncomeCost { get; set; }
        public string Client { get; set; }
        public string OptCost { get; set; }
        public DateTime RegDt { get; set; }
        public int? OrderNumber { get; set; }
        public string Comments { get; set; }
        public bool? IsBank { get; set; }
        public string UserName { get; set; }
    }
}
