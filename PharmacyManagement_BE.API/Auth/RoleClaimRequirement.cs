using Microsoft.AspNetCore.Authorization;

namespace PharmacyManagement_BE.API.Auth
{
    public class RoleClaimRequirement : IAuthorizationRequirement
    {
        public string RequiredRoleClaim { get; }

        public RoleClaimRequirement(string role)
        {
            RequiredRoleClaim = role;
        }
    }
}
