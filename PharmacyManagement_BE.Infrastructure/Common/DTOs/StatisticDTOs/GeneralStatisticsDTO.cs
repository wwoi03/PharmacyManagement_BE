﻿using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
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
        public List<StatisticDTO> StatisticRevenue { get; set; }
        public List<StatisticDTO> StatisticOrder { get; set; }
        public List<CommentDTO> GetCustomerCommentQAs { get; set; }
        public List<CommentDTO> GetCustomerCommentEvaluates { get; set; }
        public List<OrderDTO> GetCancellations { get;set; }
    }
}
