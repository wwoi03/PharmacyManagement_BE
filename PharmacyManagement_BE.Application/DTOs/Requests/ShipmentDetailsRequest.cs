﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.DTOs.Requests
{
    public class ShipmentDetailsRequest
    {
        public Guid ProductId { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ImportPrice { get; set; }
        public int Quantity { get; set; }
        public string AdditionalInfo { get; set; }
        public string Note { get; set; }
        public string ProductionBatch { get; set; }
    }
}
