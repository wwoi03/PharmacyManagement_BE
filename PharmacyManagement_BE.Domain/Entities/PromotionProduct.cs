using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class PromotionProduct :BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null;
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; } = null;
        public string AdditionalInfo { get; set; }
        public int Quantity { get; set; }
    }
}
