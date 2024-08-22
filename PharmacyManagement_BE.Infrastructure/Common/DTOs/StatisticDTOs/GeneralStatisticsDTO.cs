using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs
{
    public class GeneralStatisticsDTO
    {
        // Số sản phẩm được đặt trong ngày
        public int NumOrder { get; set; }
        //Số tiền bán được tròn 1 ngày
        public decimal SalePrice { get; set; }
        //// sản phẩm hết hạn
        //public int NumProductOutOfDate { get; set; }
    }
}
