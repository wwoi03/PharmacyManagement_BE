using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PharmacyManagement_BE.API.Auth
{
    public class AuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly string[] _policies;

        public AuthorizeFilter(IAuthorizationService authorizationService, string[] policies)
        {
            _authorizationService = authorizationService;
            _policies = policies;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            foreach (var policy in _policies)
            {
                var authorized = await _authorizationService.AuthorizeAsync(user, policy);
                if (authorized.Succeeded)
                {
                    return;
                }
            }

            context.Result = new ForbidResult();
        }
    }
}
