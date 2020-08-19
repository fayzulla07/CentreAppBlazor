using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class ProductSales
    {
        public ProductSales()
        {
            ProductReturns = new HashSet<ProductReturns>();
        }

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public double SaleCost { get; set; }
        public bool? IsOptCost { get; set; }
        public int? CustomerId { get; set; }
        public DateTime RegDt { get; set; }
        public int? UserId { get; set; }
        public string Comments { get; set; }
        public double IncomeCost { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsBank { get; set; }
        public double Kurs { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ProductReturns> ProductReturns { get; set; }
    }
}
