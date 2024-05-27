using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class OrderDetails : BaseEntity<Guid>
    {
        public Guid? OrderId { get; set; }
        public Guid? ShipmentDetailsId { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; } = null!;
        public Guid? UnitId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
    }
}
