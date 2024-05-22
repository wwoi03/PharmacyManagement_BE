using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.Securitys;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Features.ConfigFeatures.Handlers
{
    internal class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ResponseAPI<SignInResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RefreshTokenHandler(IPMEntities entities, IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._entities = entities;
            this._configuration = configuration;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<SignInResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // kiểm tra token từ Request
                if (request.AccessToken == null)
                    return new ResponseErrorAPI<SignInResponse>("Vui lòng đăng nhập.");

                var jwtSecurityToken = new JwtSecurityToken(request.AccessToken);
                var ValidTo = jwtSecurityToken.ValidTo.AddHours(7); // UTC+7 

                if (ValidTo <= DateTime.Now)
                {
                    // giải mãi token dựa vào SecretKey đã config trước đó
                    var principal = Auth.GetPrincipalFromExpiredToken(request.AccessToken, _configuration["JWT:Secret"]);

                    if (principal == null)
                        return new ResponseErrorAPI<SignInResponse>("Vui lòng đăng nhập lại.");

                    // Lấy userName từ token ra 
                    string username = principal?.Claims.ToList()[0]?.ToString()?.Split(' ')[1];

                    // kiểm tra xem RefeshToken hết hạn chưa 
                    var user = await _userManager.FindByNameAsync(username);

                    if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
                    {
                        return new ResponseErrorAPI<SignInResponse>("Vui lòng đăng nhập lại.");
                    }

                    // Tạo Token mới
                    // tạo claim
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    // Lấy danh sách role của người dùng
                    var userRoles = await _userManager.GetRolesAsync(user);

                    // thêm role vào claim
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                    }

                    // Tạo token
                    var accessToken = Auth.GetToken(authClaims, _configuration);
                    var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

                    // Tạo refesh token Cập nhật RefeshToken vào Database
                    var refreshToken = Auth.GenerateRefreshToken();
                    var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(expired));
                    _entities.CustomerService.Update((Customer)user);

                    _entities.SaveChange();

                    // Response
                    var response = _mapper.Map<SignInResponse>(user);
                    response.Token = token;

                    return new ResponseSuccessAPI<SignInResponse>(200, "Đăng nhập thành công", response);
                }

                return new ResponseErrorAPI<SignInResponse>("Vui lòng đăng nhập lại.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<SignInResponse>("Vui lòng đăng nhập.");
            }
        }
    }
}
