using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsEcommerceDTOs
{
    public class ShipmentDetailsEcommerceDTO
    {
        //Bỏ
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}
