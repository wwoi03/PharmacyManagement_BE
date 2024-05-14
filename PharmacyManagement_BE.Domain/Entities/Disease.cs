using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Disease : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
