using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    internal class LockAccountCommandHandler : IRequestHandler<LockAccountCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public LockAccountCommandHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<string>> Handle(LockAccountCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Tài khoản không tồn tại.");

                // Khoản tài khoản
                userExists.LockoutEnd = request.LockoutEnd ?? DateTime.Now.AddYears(100);

                var result = await _userManager.UpdateAsync(userExists);
                if (!result.Succeeded)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Khóa tài khoản {userExists.UserName} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
