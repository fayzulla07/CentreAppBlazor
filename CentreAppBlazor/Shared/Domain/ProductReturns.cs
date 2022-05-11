using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class ProductReturns
    {
        public int Id { get; set; }
        [Required]
        public int ProductSaleId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public decimal ReturnCost { get; set; }
        public DateTime RegDt { get; set; }
        public int? UserId { get; set; }
        public string Comments { get; set; }
        public double Kurs { get; set; }

        public virtual ProductSales ProductSale { get; set; }
        public virtual Users User { get; set; }
    }
}
