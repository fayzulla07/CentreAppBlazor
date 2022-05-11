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


        public decimal? IncomeCost { get; set; }
        public decimal? OptCost { get; set; }
        public decimal? SaleCost {get; set; }

        // For operations
        public string Comments { get; set; }
        public bool IsOptCost { get; set; } = false;
        public double Amount { get; set; } = 1;

     
        public decimal? Total 
        {
            get 
            {
                return IsOptCost == true ? OptCost * (decimal)Amount : SaleCost * (decimal)Amount;
            }
            set 
            {
               
            }
        }
        public decimal? OneTotal
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
    }
}
