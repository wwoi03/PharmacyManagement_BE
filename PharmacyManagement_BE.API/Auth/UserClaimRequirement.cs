using Microsoft.AspNetCore.Authorization;

namespace PharmacyManagement_BE.API.Auth
{
    public class UserClaimRequirement : IAuthorizationRequirement
    {
        public string RequiredUserClaim { get; }

        public UserClaimRequirement(string role)
        {
            RequiredUserClaim = role;
        }
    }
}
