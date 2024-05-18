using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ShipmentDetails : BaseEntity<Guid>
    {
        public Guid ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public DateTime ImportDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ImportPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SoldPrice { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? Note { get; set; }
    }
}
