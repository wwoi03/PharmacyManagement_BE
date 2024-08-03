using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs
{
    public class StatisticOrderDTO
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public int Cancellation { get; set; }
        public int Payment { get; set; }
    }
}
