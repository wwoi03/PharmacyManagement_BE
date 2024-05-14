using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    internal class ReceiverInformation : BaseEntity<Guid>
    {
        public string ReceiverName { get; set; }
        public string RecipientPhone { get; set; }
        public string Satus { get; set; }
        public string ProvinceOrCity { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressType { get; set; }
        public string AddressDetails { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null;
    }
}
