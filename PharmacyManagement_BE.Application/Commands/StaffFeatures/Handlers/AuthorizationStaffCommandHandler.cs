using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.StaffFeatures.Handlers
{
    internal class AuthorizationStaffCommandHandler : IRequestHandler<AuthorizationStaffCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthorizationStaffCommandHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<string>> Handle(AuthorizationStaffCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.Id.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Kiểm tra quyền hợp lệ
                foreach (var roleName in request.Roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null)
                        return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, $"Vai trò với ID {roleName} không tồn tại.");
                }

                // Lấy danh sách role hiện tại
                var currentRoles = await _userManager.GetRolesAsync(userExists);

                // Thêm role mới và xóa role cũ cho tài khoản
                var rolesToAdd = request.Roles.Except(currentRoles).ToList();
                var rolesToRemove = currentRoles.Except(request.Roles).ToList();

                await _userManager.RemoveFromRolesAsync(userExists, rolesToRemove);
                await _userManager.AddToRolesAsync(userExists, rolesToAdd);

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Cập nhật vai trò nhân viên {userExists.UserName} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
