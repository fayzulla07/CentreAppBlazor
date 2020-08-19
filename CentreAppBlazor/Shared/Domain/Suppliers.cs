using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            ProductIncoms = new HashSet<ProductIncoms>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double? HisDebt { get; set; }
        public double? MyDebt { get; set; }

        public virtual ICollection<ProductIncoms> ProductIncoms { get; set; }
    }
}
