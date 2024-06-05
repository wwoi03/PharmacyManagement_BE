using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs
{
    public class CostStatisticsShipmentDTO
    {
        public string SupplierName { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalProduct { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
