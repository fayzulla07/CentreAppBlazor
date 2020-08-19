using System;
using System.Collections.Generic;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class Users
    {
        public Users()
        {
            ProductIncoms = new HashSet<ProductIncoms>();
            ProductReturns = new HashSet<ProductReturns>();
            ProductSales = new HashSet<ProductSales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LoginName { get; set; } 
        public string Password { get; set; } 
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<ProductIncoms> ProductIncoms { get; set; }
        public virtual ICollection<ProductReturns> ProductReturns { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
