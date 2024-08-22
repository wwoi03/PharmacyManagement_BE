using PharmacyManagement_BE.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs
{
    public class PromotionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string CodePromotion { get; set; }
    }
}
