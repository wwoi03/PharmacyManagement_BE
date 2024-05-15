using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Staff : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Image { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
