using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class OrderDetails : BaseEntity<Guid>
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ShipmentDetailsId { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; } = null!;

        [Required]
        public Guid UnitId { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Status { get; set; }
    }
}
