using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using System.Security.Claims;

namespace PharmacyManagement_BE.API.Auth
{
    public class UserClaimAuthorizationHandler : AuthorizationHandler<UserClaimRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserClaimAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserClaimRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    var roleClaims = await _userManager.GetClaimsAsync(user);
                    if (roleClaims.Any(c => c.Type == "role" && c.Value == requirement.RequiredUserClaim))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
