using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
        public string Role;

        public IsAdminRequirement(string role)
        {
            this.Role = role;
        }
    }
}
