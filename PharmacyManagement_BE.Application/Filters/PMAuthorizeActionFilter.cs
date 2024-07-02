using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Filters
{
    public class PMAuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private IConfiguration _configuration;
        private IPMEntities _entities;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _roles;

        public PMAuthorizeActionFilter(string roles, IConfiguration configuration, IPMEntities entities, UserManager<ApplicationUser> userManager)
        {
            this._configuration = configuration;
            this._entities = entities;
            this._userManager = userManager;
            this._roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                // Kiểm tra có token
                var username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ResponseErrorAPI<string>("Bạn không có quyền truy cập tài nguyên này"));

                    return;
                }
                
                // kiểm tra user tồn tại trong hệ thống
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ResponseErrorAPI<string>(StatusCodes.Status401Unauthorized,"Bạn không có quyền truy cập tài nguyên này"));

                    return;
                }

                // lấy danh sách role của user
                var userRoles = await _userManager.GetRolesAsync(user);

                // Lấy danh sách role tài nguyên yêu cầu
                string[] requiredRoles = _roles.Split(',');

                // kiểm tra user có quyền truy cập tài nguyên
                bool hasPermission = requiredRoles.Any(requiredRole => userRoles.Contains(requiredRole));

                if (!hasPermission)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ResponseErrorAPI<string>(StatusCodes.Status401Unauthorized, "Bạn không có quyền truy cập tài nguyên này"));

                    return;
                }
            }
        }
    }
}
