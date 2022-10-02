using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class CustomerTypes
    {
        public CustomerTypes()
        {
            Customers = new HashSet<Customers>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Discount { get; set; } // discount in percent

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
