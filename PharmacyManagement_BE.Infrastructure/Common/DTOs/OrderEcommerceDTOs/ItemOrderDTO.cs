using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderEcommerceDTOs
{
    public class ItemOrderDTO
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CodeOrder { get; set; }
        public string Status { get; set; }
        public decimal FinalAmount { get; set; }
        public int ProductQuantity { get; set; }
        public string NameFirstProduct { get; set; }
        public string ImageFirstProduct { get; set; }
        public decimal PriceFirstProduct { get; set; }
        public int QuantityFirstProduct { get; set; }
        public string UnitFirstProduct { get; set; }
    }
}
