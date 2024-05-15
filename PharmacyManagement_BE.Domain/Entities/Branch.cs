using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Branch : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string ProvinceOrCity { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string AddressDetails { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
    }
}
