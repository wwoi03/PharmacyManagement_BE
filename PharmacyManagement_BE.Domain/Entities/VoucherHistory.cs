using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class VoucherHistory: BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null;
        public Guid VoucherId { get; set; }
        public Voucher Voucher { get; set; } = null;
        public DateTime AppliedDate { get; set; }
        public double DiscountApplied { get; set; }
    }
}
