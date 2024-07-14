using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs
{
    public class ShipmentDetailsUnitDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
    }
}
