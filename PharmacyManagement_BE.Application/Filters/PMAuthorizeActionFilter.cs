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
        private readonly string _functionCode;
        private readonly string _permission;

        private IConfiguration _configuration;
        private IPMEntities _entities;

        public PMAuthorizeActionFilter(string functionCode, string permission, IConfiguration configuration, IPMEntities entities)
        {
            this._configuration = configuration;
            this._entities = entities;
            this._functionCode = functionCode;
            this._permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                // B1: Lấy thông tin User từ Token
                var user = new ApplicationUser
                {
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                };

                // B2: kiểm tra người dùng có quyền truy cập tài nguyên


                if (string.IsNullOrEmpty(user.UserName))
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new ResponseErrorAPI<string>("Bạn không có quyền truy cập tài nguyên này"));

                    return;
                }
            }
        }
    }
}
