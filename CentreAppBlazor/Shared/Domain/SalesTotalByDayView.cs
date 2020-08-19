using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class SalesTotalByDayView
    {
        public DateTime? RegDt { get; set; }
        public double? IncomeCost { get; set; }
        public double? SaleTotal { get; set; }
    }
}
