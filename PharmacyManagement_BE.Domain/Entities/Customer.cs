using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Customer : ApplicationUser
    {
        public string? FirstName { get; set; }
        public string? Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string? Image { get; set; } 
    }
}
