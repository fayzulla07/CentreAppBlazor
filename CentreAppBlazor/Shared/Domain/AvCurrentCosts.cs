using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class AvCurrentCosts
    {
        public int ProductId { get; set; }
        public double? IncomeCost { get; set; }
        public double? OptCost { get; set; }
        public double? SaleCost { get; set; }
    }
}
