using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Extentions
{
    public class TokenValidationExtention
    {
        private readonly RequestDelegate _next;

        public TokenValidationExtention(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;

                // Kiểm tra người dùng đã đăng nhập chưa
                var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

                if (username == null)
                {
                    await _next(context);
                    return;
                }

                // Kiểm tra Token còn được cấp phép hay không
                var user = await userManager.FindByNameAsync(username);

                if (user != null && user.RefreshToken == null)
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            await _next(context);
        }
    }
}
