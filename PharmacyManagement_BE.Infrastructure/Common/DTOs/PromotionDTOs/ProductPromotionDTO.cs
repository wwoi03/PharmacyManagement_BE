using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs
{
    public class ProductPromotionDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public string AdditionalInfo { get; set; }
        public int Quantity { get; set; }

        public List<PromotionProgramDTO>? PromotionPrograms { get; set; }

    }
}
