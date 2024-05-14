using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class PromotionProgram : BaseEntity<Guid>
    {
        public Guid PromotionProductId { get; set; }
        public PromotionProduct PromotionProduct { get; set; } = null;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null;
        public int Quantity { get; set; }
    }
}
