using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements
{
    public class IsStaffManagerRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public IsStaffManagerRequirement(string role)
        {
            this.Role = role;
        }
    }
}
