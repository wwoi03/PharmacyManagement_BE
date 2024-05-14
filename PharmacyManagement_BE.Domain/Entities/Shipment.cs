using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Shipment : BaseEntity<Guid>
    {
        public DateTime ImportDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
    }
}
