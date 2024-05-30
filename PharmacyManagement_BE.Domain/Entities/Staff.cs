using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Staff : ApplicationUser
    {
        [StringLength(1000)]
        public string? Address { get; set; }

        public Guid? BranchId { get; set; }
    }
}
