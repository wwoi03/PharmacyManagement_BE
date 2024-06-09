using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs
{
    public class ListShipmentDetailsDTO
    {
        public Guid ShipmentDetailsId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ImportPrice { get; set; }
        public int Quantity { get; set; }
        public int Sold { get; set; }
    }
}
