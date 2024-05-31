using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.DTOs.Responses
{
    public class ShipmentResponse
    {
        public Guid Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; }
        public int TotalProduct { get; set; }
        public int TotalQuantity { get; set; }
    }
}
