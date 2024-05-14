using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ShipmentDetailsId { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; } = null!;
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
