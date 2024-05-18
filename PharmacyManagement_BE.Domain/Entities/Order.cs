using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public string? OrdererName { get; set; }
        public string? ReceiverName { get; set; } 
        public string? RecipientPhone { get; set; } 
        public string? Email { get; set; } 
        public string? ProvinceOrCity { get; set; } 
        public string? District { get; set; } 
        public string? Ward { get; set; } 
        public string? AddressDetails { get; set; } 
        public decimal TotalDiscount { get; set; }
        public decimal TransportFee { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string? Note { get; set; } 
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; } 
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentStatus { get; set; } 
        public string? AccountNumber { get; set; } 
        public string? BankName { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
    }
}
