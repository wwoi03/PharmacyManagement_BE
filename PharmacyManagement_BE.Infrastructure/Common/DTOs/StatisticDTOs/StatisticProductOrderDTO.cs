using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs
{
    public class StatisticProductOrderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CartView { get; set; }
        public string Image { get; set; }
        public int Num { get; set; }
    }
}
