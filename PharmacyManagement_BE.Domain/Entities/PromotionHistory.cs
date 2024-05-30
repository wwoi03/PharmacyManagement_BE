using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class PromotionHistory
    {
        [Key]
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; } = null!;

        [Key]
        public Guid OrderDetailsId { get; set; }
        public OrderDetails OrderDetails { get; set; } = null!;

        [Required]
        public DateTime AppliedDate { get; set; }

        public double DiscountApplied { get; set; }
    }
}
