using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class PaymentMethod : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
    }
}
