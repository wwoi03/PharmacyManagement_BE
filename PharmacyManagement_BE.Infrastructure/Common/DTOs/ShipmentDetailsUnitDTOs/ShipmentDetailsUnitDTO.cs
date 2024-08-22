using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs
{
    public class ShipmentDetailsUnitDTO
    {
        public Guid ShipmentDetailsId { get; set; }
        public int QuantityInStock { get; set; }
        public Guid? UnitId { get; set; }
        public string? CodeUnit { get; set; }
        public string UnitName { get; set; }
        public decimal SalePrice { get; set; }
        public int UnitCount { get; set; }
        public int Level { get; set; }
    }
}
