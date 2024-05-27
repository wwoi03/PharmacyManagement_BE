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
    internal class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UpdateStaffCommandHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateStaffCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra thông tin
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, validation.Message);

                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.Id.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Nhân viên không tồn tại.");

                // Kiểm tra tên đăng nhập tồn tại
                var usernameExists = await _userManager.FindByNameAsync(request.UserName);

                if (usernameExists != null && userExists.Id != request.Id)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Tên đăng nhập đã tồn tại");

                // Kiểm tra quyền hợp lệ
                foreach (var roleName in request.Roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null)
                        return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, $"Vai trò với ID {roleName} không tồn tại.");
                }

                // Kiểm tra chi nhánh
                var branchExists = await _entities.BranchService.GetById(request.BranchId);

                if (branchExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Chi nhánh không tồn tại.");

                // Cập nhật tài khoản
                var staff = (Staff)await _userManager.FindByIdAsync(request.Id.ToString());
                staff.FullName = request.FullName;
                staff.UserName = request.UserName;
                staff.PhoneNumber = request.PhoneNumber;
                staff.Email = request.Email;
                staff.Gender = request.Gender;
                staff.Birthday = request.Birthday ;
                staff.Address = request.Address ;
                staff.Image = request.Image ;
                staff.BranchId = request.BranchId;

                var result = await _userManager.UpdateAsync(staff);

                if (!result.Succeeded)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, result.Errors.First().Description);

                // cập nhật mật khẩu
                if (!(await _userManager.CheckPasswordAsync(staff, request.Password)))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(staff);
                    var passwordChangeResult = await _userManager.ResetPasswordAsync(staff, token, request.Password);
                }

                // Thêm role mới và xóa role cũ cho tài khoản
                var currentRoles = await _userManager.GetRolesAsync(staff);
                var rolesToAdd = request.Roles.Except(currentRoles).ToList();
                var rolesToRemove = currentRoles.Except(request.Roles).ToList();
                await _userManager.RemoveFromRolesAsync(staff, rolesToRemove);
                await _userManager.AddToRolesAsync(staff, rolesToAdd);

                return new ResponseErrorAPI<string>(StatusCodes.Status200OK, "Chỉnh sửa nhân viên thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Lỗi hệ thống.");
            }
        }
    }
}
