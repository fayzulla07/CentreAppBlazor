using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class Units
    {
        public Units()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
