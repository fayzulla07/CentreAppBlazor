using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class Products
    {
        public Products()
        {
            ProductIncoms = new HashSet<ProductIncoms>();
            ProductSales = new HashSet<ProductSales>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? ProductTypeId { get; set; }
        public double RemainCount { get; set; }
        public int? UnitId { get; set; }
        public int Limit { get; set; }

        public byte[] Photo { get; set; }
        public string Extension { get; set; }

        public virtual ProductTypes ProductType { get; set; }
        public virtual Units Unit { get; set; }
        public virtual ICollection<ProductIncoms> ProductIncoms { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }

    }
}
