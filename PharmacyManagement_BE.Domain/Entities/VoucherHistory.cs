using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class VoucherHistory
    {
        [Key]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        [Key]
        public Guid VoucherId { get; set; }
        public Voucher Voucher { get; set; } = null!;
        public DateTime AppliedDate { get; set; }
        public double DiscountApplied { get; set; }
    }
}
