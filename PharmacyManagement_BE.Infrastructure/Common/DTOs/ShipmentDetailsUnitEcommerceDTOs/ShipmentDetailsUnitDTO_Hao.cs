using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs
{
    public class ShipmentDetailsUnitDTO_Hao
    {
        public Guid ShipmentDetailsId { get; set; }
        public Guid UnitId { get; set; }
        public string CodeUnit { get; set; }
        public string UnitName { get; set; }
        public decimal SalePrice { get; set; }
    }
}
