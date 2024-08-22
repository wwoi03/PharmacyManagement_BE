using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs
{
    public class ProductPromotionRequestDTO
    {
        public List<Guid>? ProductId { get; set; }
        public string AdditionalInfo { get; set; }
        public int Quantity { get; set; }

        public List<PromotionProgramRequestDTO>? promotionProgramRequest { get; set; }

    }
}
