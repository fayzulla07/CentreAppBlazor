using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class AvProfit
    {
        public double TotalIncome { get; set; }
        public double TotalSale { get; set; }
        public double TotalOpt { get; set; }
        public double TotalSaleProfit
        {
            get { return TotalSale - TotalIncome; }
        }
        public double TotalOptProfit
        {
            get { return TotalOpt - TotalIncome; }
        }

        public double TotalIncomeTG { get; set; }
        public double TotalSaleTG { get; set; }
        public double TotalOptTG { get; set; }

        public double TotalSaleProfitTG
        {
            get { return TotalSaleTG - TotalIncomeTG; }
        }
        public double TotalOptProfitTG
        {
            get { return TotalOptTG - TotalIncomeTG; }
        }
    }
}
