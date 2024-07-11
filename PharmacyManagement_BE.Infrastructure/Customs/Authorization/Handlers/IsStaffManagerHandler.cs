using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization.Handlers
{
    public class IsStaffManagerHandler : AuthorizationHandler<IsStaffManagerRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public IsStaffManagerHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsStaffManagerRequirement requirement)
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
                    var currentRolesName = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    var currentNormalizedRoleNames = allRoles.Where(role => currentRolesName.Contains(role.Name)).Select(role => role.NormalizedName.ToUpper()).ToList();

                    // Kiểm tra Role
                    if (currentNormalizedRoleNames.Contains(requirement.Role.ToUpper()))
                        context.Succeed(requirement);
                }
            }
        }
    }
}
