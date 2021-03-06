﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public partial class ProductIncoms
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public double IncomeCost { get; set; }

        [Required]
        public double SaleCost { get; set; }

        [Required]
        public double OptCost { get; set; }
        public DateTime? ProductionDt { get; set; }
        public int? SupplierId { get; set; }
        public DateTime RegDt { get; set; }
        public int? UserId { get; set; }
        public string Comments { get; set; }
        public double Kurs { get; set; }

        public virtual Products Product { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual Users User { get; set; }
    }
}
