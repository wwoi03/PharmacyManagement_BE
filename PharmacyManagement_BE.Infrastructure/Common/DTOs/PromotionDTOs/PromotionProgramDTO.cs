using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs
{
    public class PromotionProgramDTO
    {
        public Guid PromotionProductId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        //tên sản phẩm tặng kèm
        public string ProductName { get; set; }
        public string CodeProduct { get; set; }
    }
}
