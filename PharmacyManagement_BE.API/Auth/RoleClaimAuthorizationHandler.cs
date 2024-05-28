using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using System.Linq;
using System.Security.Claims;

namespace PharmacyManagement_BE.API.Auth
{
    public class RoleClaimAuthorizationHandler : AuthorizationHandler<RoleClaimRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleClaimAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleClaimRequirement requirement)
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
                    if (roleClaims.Any(c => c.Type == "role" && c.Value == requirement.RequiredRoleClaim))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
