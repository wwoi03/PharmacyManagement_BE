using MediatR;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AccountFeatures.Handlers
{
    internal class RevokeTokenHandler : IRequestHandler<RevokeTokenRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeTokenHandler(IPMEntities entities, UserManager<ApplicationUser> userManager)
        {
            this._entities = entities;
            this._userManager = userManager;
        }

        public async Task<ResponseAPI<string>> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
        {
            // B1: Kiểm tra người dùng tồn tại
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
                return new ResponseErrorAPI<string>("Người dùng không tồn tại.");

            // B2: xóa Refresh Token
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return new ResponseSuccessAPI<string>("Thành công");
        }
    }
}
