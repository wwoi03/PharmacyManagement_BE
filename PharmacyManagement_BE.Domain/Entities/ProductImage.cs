﻿using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ProductImage : BaseEntity<Guid>
    {
        [Required]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string? Image { get; set; }
    }
}
