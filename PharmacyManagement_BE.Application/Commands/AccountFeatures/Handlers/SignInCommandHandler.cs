using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AccountFeatures.Handlers
{
    internal class SignInCommandHandler : IRequestHandler<SignInCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly UserManager<ApplicationUser> _userManager;

        public SignInCommandHandler(IPMEntities entities, UserManager<ApplicationUser> userManager)
        {
            this._entities = entities;
            this._userManager = userManager;
        }

        public async Task<ResponseAPI<string>> Handle(SignInCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra đăng nhập hợp lệ
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                    return new ResponseErrorAPI<string>(StatusCodes.Status401Unauthorized, "Thông tin tài khoản hoặc mật khẩu không chính xác.");

                // Kiểm tra xem tài khoản có bị khóa không
                if (await _userManager.IsLockedOutAsync(user))
                    return new ResponseErrorAPI<string>(StatusCodes.Status403Forbidden, $"Tài khoản của bạn đã bị khóa đến ngày {user.LockoutEnd}.");

                // Tạo claim
                var authClaims = await _entities.TokenService.CreateAuthClaim(user);

                // Tạo token
                DateTime time = DateTime.Now;
                var accessToken = await _entities.TokenService.GetToken(authClaims, time);
                var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

                // Tạo refesh token 
                var refreshToken = await _entities.TokenService.GenerateRefreshToken();
                var refreshTokenExpiredTime = await _entities.TokenService.GetRefreshTokenExpiryTime(time);

                // Cập nhật RefeshToken vào Database
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = refreshTokenExpiredTime;
                _entities.StaffService.Update((Staff)user);

                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Đăng nhập thành công.", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
