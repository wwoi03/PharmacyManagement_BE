using PharmacyManagement_BE.Domain.Entities.Bases;
using PharmacyManagement_BE.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Voucher : BaseEntity<Guid>
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountValue { get; set; }
        public VoucherType DiscountType { get; set; }
        public int Quantity { get; set; }
    }
}
