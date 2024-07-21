﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs
{
    public class ItemCartDTO
    {
        public Guid CardId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UnitId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string UnitName { get; set; }
        public int Quantity { get; set; }
    }
}