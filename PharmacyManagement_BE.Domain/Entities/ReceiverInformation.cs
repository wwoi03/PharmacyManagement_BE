using PharmacyManagement_BE.Domain.Entities.Bases;
using PharmacyManagement_BE.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ReceiverInformation : BaseEntity<Guid>
    {
        public string ReceiverName { get; set; } = string.Empty;
        public string RecipientPhone { get; set; } = string.Empty;
        public string Satus { get; set; } = string.Empty;
        public string ProvinceOrCity { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public AddressType AddressType { get; set; }
        public string AddressDetails { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
