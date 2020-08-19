using CentreAppBlazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Dto
{
    public class SalesReturnsbyDayDto
    {
        public DateTime RegDt { get; set; } = DateTime.Now;
        public double IncomeCost { get; set; }
        public double SaleTotal { get; set; }
        public double ReturnCost { get; set; }
        public double Profit 
        {
            get 
            { 
                return SaleTotal - IncomeCost; 
            }
        }
        public AvProfit AvProfit { get; set; }
    }
}
