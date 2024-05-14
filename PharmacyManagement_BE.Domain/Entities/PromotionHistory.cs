using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class PromotionHistory : BaseEntity<Guid>
    {
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; } = null;
        public Guid OrderDetailsId { get; set; }
        public OrderDetails OrderDetails { get; set; } = null;
        public string AppliedDate { get; set; }
        public double DiscountApplied { get; set; }
    }
}
