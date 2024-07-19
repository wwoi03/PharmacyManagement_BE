using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.SupplierDTOs
{
    public class SelectSupplierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeSupplier { get; set; }
    }
}
