using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Staff : ApplicationUser
    {
        public string? Address { get; set; }
        public Guid? BranchId { get; set; }
    }
}
