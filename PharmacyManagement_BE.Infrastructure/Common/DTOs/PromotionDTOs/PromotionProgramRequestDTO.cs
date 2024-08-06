using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs
{
    public class PromotionProgramRequestDTO
    {
        public List<Guid>? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
