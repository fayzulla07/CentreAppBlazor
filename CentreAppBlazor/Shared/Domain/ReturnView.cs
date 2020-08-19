using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class ReturnView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ReturnCost { get; set; }
        public string AmountUnit { get; set; }
        public DateTime RegDt { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }
        public string Comments { get; set; }
    }
}
