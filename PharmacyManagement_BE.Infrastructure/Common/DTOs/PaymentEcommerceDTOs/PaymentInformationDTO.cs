﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs
{
    public class PaymentInformationDTO
    {
        public string OrderType { get; set; }
        public string CodeOrder { get; set; }
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
