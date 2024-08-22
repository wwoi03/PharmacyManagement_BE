using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs
{
    public class OrderDetailsDTO
    {
        public Guid OrderId { get; set; }
        public Guid ShipmentDetailsId { get; set; }
        public ShipmentDetailsOrderDTO? ShipmentDetails { get; set; }
        public Guid UnitId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
    }
}
