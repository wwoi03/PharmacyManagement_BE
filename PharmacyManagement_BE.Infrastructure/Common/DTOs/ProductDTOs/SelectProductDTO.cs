using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs
{
    public class SelectProductDTO 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeMedicine { get; set; }
    }
}
