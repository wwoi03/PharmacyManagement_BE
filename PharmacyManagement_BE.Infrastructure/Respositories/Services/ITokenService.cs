using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GetToken(List<Claim> authClaims, DateTime time);
        Task<string> GenerateRefreshToken();
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task<List<Claim>> CreateAuthClaim(ApplicationUser user);
        Task<DateTime> GetRefreshTokenExpiryTime(DateTime time);
        Task AddToBlacklistAsync(string token, DateTime expiry);
        Task<bool> IsBlacklistedAsync(string token);
    }
}
