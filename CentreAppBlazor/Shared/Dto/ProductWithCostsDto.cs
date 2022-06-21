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

        public byte[] Photo { get; set; }
        public string Extension { get; set; }

        public double? IncomeCost { get; set; }
        public double? OptCost { get; set; }
        public double? SaleCost {get; set; }

        // For operations
        public string Comments { get; set; }
        public bool IsOptCost { get; set; } = false;
        public double Amount { get; set; } = 1;

     
        public double? Total 
        {
            get 
            {
                return IsOptCost == true ? OptCost * Amount : SaleCost * Amount;
            }
            set 
            {
               
            }
        }
        public double? OneTotal
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
