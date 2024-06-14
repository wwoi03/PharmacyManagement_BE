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
    public class Order : BaseEntity<Guid?>
    {
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [StringLength(100)]
        public string? OrdererName { get; set; }

        [StringLength(100)]
        public string? ReceiverName { get; set; }


        [StringLength(50)]
        public string? RecipientPhone { get; set; }


        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? ProvinceOrCity { get; set; }

        [StringLength(50)]
        public string? District { get; set; }

        [StringLength(50)]
        public string? Ward { get; set; }

        [StringLength(1000)]
        public string? AddressDetails { get; set; } 

        public decimal TotalDiscount { get; set; }

        public decimal TransportFee { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal FinalAmount { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Status { get; set; } 

        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? PaymentStatus { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? AccountNumber { get; set; }

        [StringLength(50)]
        public string? BankName { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = null!;

        public Guid? StaffId { get; set; }

        public Guid? BranchId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CodeOrder { get; set; }
    }
}
