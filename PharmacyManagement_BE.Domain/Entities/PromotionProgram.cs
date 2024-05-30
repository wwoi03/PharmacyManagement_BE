using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class PromotionProgram
    {
        [Key]
        public Guid PromotionProductId { get; set; }
        public PromotionProduct PromotionProduct { get; set; } = null!;

        [Key]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
