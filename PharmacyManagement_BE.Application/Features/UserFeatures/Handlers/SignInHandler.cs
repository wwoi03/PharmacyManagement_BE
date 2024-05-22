using AutoMapper;
using MediatR;
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

namespace PharmacyManagement_BE.Application.Features.UserFeatures.Handlers
{
    public class SignInHandler : IRequestHandler<SignInRequest, ResponseAPI<SignInResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SignInHandler(IPMEntities entities, UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this._entities = entities;
            this._configuration = configuration;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<ResponseAPI<SignInResponse>> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // B1: Kiểm tra ràng buộc
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<SignInResponse>(validation.Message);

                // B2: Kiểm tra đăng nhập hợp lệ
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                    return new ResponseErrorAPI<SignInResponse>("Thông tin tài khoản hoặc mật khẩu không chính xác.");

                // B3: tạo claim
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // B4: Lấy danh sách role của người dùng
                var userRoles = await _userManager.GetRolesAsync(user);

                // B5: thêm role vào claim
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }

                // B6: Tạo token
                var accessToken = Auth.GetToken(authClaims, _configuration);
                var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

                // B7: Tạo refesh token Cập nhật RefeshToken vào Database
                var refreshToken = Auth.GenerateRefreshToken();
                var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToInt32(expired));
                _entities.CustomerService.Update((Customer)user);

                _entities.SaveChange();

                // B8: Response
                var response = _mapper.Map<SignInResponse>(user);
                response.Token = token;

                return new ResponseSuccessAPI<SignInResponse>(200, "Đăng nhập thành công", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseErrorAPI<SignInResponse>("Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
