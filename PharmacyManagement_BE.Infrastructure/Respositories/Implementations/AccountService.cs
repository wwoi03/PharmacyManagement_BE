using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpContext _httpContext;
        private ApplicationUser _user;
        private Staff _staff;

        public AccountService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task GetUserAsync()
        {
            if (_user == null)
            {
                var username = _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (username != null)
                {
                    _user = await _userManager.FindByNameAsync(username);
                    // _staff = (Staff)_user;
                }
            }
        }

        public async Task<Guid> GetAccountId()
        {
            await GetUserAsync();
            return (Guid)(_user != null ? _user.Id : Guid.Empty);
        }

        public async Task<Guid> GetBranchId()
        {
            await GetUserAsync();
            if (_staff == null)
            {
                _staff = (Staff)_user;
            }

            return (Guid)(_staff != null ? _staff.BranchId : Guid.Empty);
        }
    }
}
