﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ProductUnit
    {
        [Key]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Key]
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
        public decimal SalePrice { get; set; }
    }
}
