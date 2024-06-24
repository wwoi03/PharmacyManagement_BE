using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            // Kiểm tra Token
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].ToString().Split(' ').Last();

            // Kiểm tra Token rỗng hoặc có trong Blacklist
            if (string.IsNullOrEmpty(token))
            {
                await _next(context);
                return;
            }

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
                else if (user != null) // Kiểm ra token đã bị hủy
                {
                    var expiredRefreshToken = int.Parse(configuration["JWT:RefreshTokenValidityInDays"]);
                    var expiredToken = int.Parse(configuration["JWT:TokenValidityInMinutes"]);

                    var expirationClaim = identity.FindFirst("exp");

                    if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var exp))
                    {
                        // Chuyển đổi giá trị exp từ giây sang DateTime theo múi giờ Việt Nam
                        var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(exp).ToOffset(TimeSpan.FromHours(7));

                        var userTokenOriginTimeString = (expirationDateTime - TimeSpan.FromMinutes(expiredToken))
                            .ToString("yyyy-MM-dd HH:mm:ss");
                        var userRefreshTokenOriginTimeString = (user.RefreshTokenExpiryTime.Value - TimeSpan.FromDays(expiredRefreshToken))
                            .ToString("yyyy-MM-dd HH:mm:ss"); 

                        var userTokenOriginTime = DateTime.ParseExact(userTokenOriginTimeString, "yyyy-MM-dd HH:mm:ss", null);
                        var userRefreshTokenOriginTime = DateTime.ParseExact(userRefreshTokenOriginTimeString, "yyyy-MM-dd HH:mm:ss", null);

                        // (Kiểm thời gian token - thời gian token sử dụng trong hệ thống) < (Thời gian RefreshToken - Thời gian sử dụng RefreshToken trong hệ thống)														
                        if (userTokenOriginTime < userRefreshTokenOriginTime)
                        {
                            // Lưu Token vào Blacklist

                            context.Response.StatusCode = 401;
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
