using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PharmacyManagement_BE.Domain.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.Securitys
{
    public class AuthorizeOrFilter : IAsyncAuthorizationFilter
    {
        private readonly string[] _roles;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizeOrFilter(string roles, UserManager<ApplicationUser> userManager)
        {
            _roles = roles.Split(',');
            _userManager = userManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Kiểm tra user tồn tại trong hệ thống
            var userId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Lấy danh sách role của user
            var userRoles = await _userManager.GetRolesAsync(user);

            if (!_roles.Any(role => userRoles.Contains(role)))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
