using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class Branch : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string ProvinceOrCity { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetails { get; set; }
        public string Phone { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null;
    }
}
