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
        [Required]
        public Guid ShipmentId { get; set; }
        public Shipment Shipment { get; set; } = null!;

        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required]
        public DateTime ManufactureDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public decimal ImportPrice { get; set; }

        public int Quantity { get; set; }

        public int Sold { get; set; }

        [StringLength(int.MaxValue)]
        public string? AdditionalInfo { get; set; }

        [StringLength(int.MaxValue)]
        public string? Note { get; set; }

        [StringLength(100)]
        public string? ProductionBatch { get; set; }

        public Guid UnitId { get; set; }
    }
}
