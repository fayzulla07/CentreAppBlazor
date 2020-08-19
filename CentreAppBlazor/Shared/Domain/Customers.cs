using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class Customers
    {
        public Customers()
        {
            ProductSales = new HashSet<ProductSales>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double? GeoLatitude { get; set; }
        public double? GeoLongitude { get; set; }
        public double? GeoAltitude { get; set; }
        public string PhoneNumber { get; set; }
        public int? CustomerTypeId { get; set; }

        public virtual CustomerTypes CustomerType { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
