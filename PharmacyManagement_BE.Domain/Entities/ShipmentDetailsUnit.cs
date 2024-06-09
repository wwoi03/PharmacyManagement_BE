using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ShipmentDetailsUnit
    {
        [Key]
        public Guid ShipmentDetailsId { get; set; }
        public ShipmentDetails ShipmentDetails { get; set; } = null!;

        [Key]
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public decimal SalePrice { get; set; }
    }
}
