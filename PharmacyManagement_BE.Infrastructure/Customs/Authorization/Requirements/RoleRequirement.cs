using Microsoft.AspNetCore.Authorization;

namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        
        public string? RequiredRole { get; set; }
        public List<string>? RequiredRoleClaim { get; set; }
        public List<string>? RequiredUserClaim { get; set; }

        public RoleRequirement(string requiredRole, List<string> requiredRoleClaim, List<string> requiredUserClaim)
        {
            RequiredRole = requiredRole ?? null;
            RequiredRoleClaim = requiredRoleClaim ?? null;
            RequiredUserClaim = requiredUserClaim ?? null;
        }
    }
}
