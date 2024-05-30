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
    public class Shipment : BaseEntity<Guid>
    {

        [Required]
        public DateTime ImportDate { get; set; }

        [StringLength(int.MaxValue)]
        public string? Note { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }

        [Required]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        [Required]
        public Guid BranchId { get; set; }

        [Required]
        public Guid? StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
    }
}
