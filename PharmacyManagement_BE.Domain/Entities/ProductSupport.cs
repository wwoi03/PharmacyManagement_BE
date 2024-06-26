﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ProductSupport 
    {
        [Key]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Key]
        public Guid SupportId { get; set; }
        public Support Support { get; set; } = null!;
    }
}
