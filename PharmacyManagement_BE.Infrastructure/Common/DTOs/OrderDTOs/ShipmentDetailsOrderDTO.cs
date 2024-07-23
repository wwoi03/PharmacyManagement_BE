using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs
{
    public class ShipmentDetailsOrderDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductOrderDTO? Product { get; set; }
        public Guid UnitId { get; set; }
    }
}
