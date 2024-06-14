using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs
{
    public class ListProductDTO
    {
        public Guid Id { get; set; }
        public string CodeMedicine { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public string BrandOrigin { get; set; }
        public string ShortDescription { get; set; }
    }
}
