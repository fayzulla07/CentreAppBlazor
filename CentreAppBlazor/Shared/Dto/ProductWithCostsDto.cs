using CentreAppBlazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentreAppBlazor.Shared.Dto
{
    public class ProductWithCostsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double RemainCount { get; set; }
        public string UnitName { get; set; }


        public decimal IncomeCost { get; set; }
        public decimal OptCost { get; set; }
        public decimal SaleCost {get; set; }
        public decimal Kurs { get; set; } = 1;
        // For operations
        public string Comments { get; set; }
        public bool IsOptCost { get; set; } = false;
        public double Amount { get; set; }
        public double AmountLeft { get; set; }
        public double Volume { get; set; }

        private decimal costTemp;
        public decimal CostTemp
        {
            get { return costTemp=costTemp==0?SaleCost*Kurs:costTemp; }
            set { costTemp = value; SaleCost = value / Kurs; }
        }

        public double Total 
        {
            get 
            {
                return IsOptCost == true ? Convert.ToDouble(OptCost) * Amount : Convert.ToDouble(SaleCost) * Amount;
            }
            set { }
        }
        public decimal OneTotal
        {
            get 
            {
                if (IsOptCost) return OptCost;
                else return SaleCost;
            }
            set 
            {
                if (IsOptCost)  OptCost = value;
                else  SaleCost = value;
            }
        }

        public double VolumeTotal
        {
            set
            {
                if(value == 0)
                {
                    Amount = 0;
                    return;
                }
                Amount = value / Volume;
            }
            get
            {
                if (Volume == 0) return 0;
                return Volume * Amount;
            }
        }
    }
}
