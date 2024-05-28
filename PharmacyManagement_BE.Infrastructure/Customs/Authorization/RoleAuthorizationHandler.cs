using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using System.Linq;
using System.Security.Claims;

namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;

                // Kiểm tra người dùng đã đang nhập
                var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                if (username == null)
                {
                    context.Fail();
                    return;
                }

                // Kiểm tra Roles, UserClaims, RoleClaims của người dùng
                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    // Lấy Role
                    var role = await _roleManager.FindByNameAsync(requirement.RequiredRole);

                    // Kiểm tra Role
                    if (requirement.RequiredRole != null)
                    {
                        if (!roles.Contains(requirement.RequiredRole))
                        {
                            context.Fail();
                            return;
                        }
                    }

                    // Kiểm tra Role Claim
                    if (requirement.RequiredRoleClaim != null && requirement.RequiredRoleClaim.Count > 0)
                    {
                        // Lấy RoleClaim
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        if (!requirement.RequiredRoleClaim.All(rc => roleClaims.Any(c => c.Type == "Permission" && c.Value == rc)))
                        {
                            context.Fail();
                            return;
                        }
                    }

                    // Kiểm tra User Claim
                    if (requirement.RequiredUserClaim != null && requirement.RequiredUserClaim.Count > 0)
                    {
                        // Lấy UserClaim
                        var userClaims = await _userManager.GetClaimsAsync(user);
                        if (!requirement.RequiredUserClaim.All(uc => userClaims.Any(c => c.Type == "Permission" && c.Value == uc)))
                        {
                            context.Fail();
                            return;
                        }
                    }

                    // Đủ quyền truy cập
                    context.Succeed(requirement);
                }
            }
        }
    }
}
