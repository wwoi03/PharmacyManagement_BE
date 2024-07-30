using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs
{
    public class ItemProductDTO
    {
        public Guid ProductId { get; set; }
        public Guid ShipmentDetailsId { get; set; }
        public string ProductName { get; set; }
        public string Specifications { get; set; }
        public string ProductImage { get; set; }
        public List<ShipmentDetailsUnitDTO> ShipmentDetailsUnits { get; set; }
        public decimal Discount { get; set; }
    }
}
