using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
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
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);

                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.Id.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Kiểm tra tên đăng nhập tồn tại
                var usernameExists = await _userManager.FindByNameAsync(request.UserName);

                if (usernameExists != null && userExists.Id != request.Id)
                {
                    validation.Obj = "userName";
                    validation.Message = "Tên đăng nhập đã tồn tại.";
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                }

                // Kiểm tra chi nhánh
                if (request.BranchId != null)
                {
                    var branchExists = await _entities.BranchService.GetById(request.BranchId);

                    if (branchExists == null)
                    {
                        validation.Obj = "default";
                        validation.Message = "Chi nhánh không tồn tại.";
                        return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                    }
                }

                // Kiểm tra quyền hợp lệ
                foreach (var normalRoleName in request.Roles)
                {
                    var role = await _roleManager.FindByNameAsync(normalRoleName);
                    if (role == null)
                    {
                        validation.Obj = "roles";
                        validation.Message = $"Vai trò với ID {normalRoleName} không tồn tại.";
                        return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                    }
                }

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
                staff.BranchId = request.BranchId == null ? await _entities.AccountService.GetBranchId() : request.BranchId;

                var result = await _userManager.UpdateAsync(staff);

                if (!result.Succeeded)
                {
                    validation = ValidUser(result);
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                }

                // cập nhật mật khẩu
                if (!(await _userManager.CheckPasswordAsync(staff, request.Password)))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(staff);
                    var passwordChangeResult = await _userManager.ResetPasswordAsync(staff, token, request.Password);
                }

                // Thêm role mới và xóa role cũ cho tài khoản
                var currentRolesName = await _userManager.GetRolesAsync(staff);
                var allRoles = _roleManager.Roles.ToList();
                var currentNormalizedRoleNames = allRoles.Where(role => currentRolesName.Contains(role.Name)).Select(role => role.NormalizedName).ToList();
                var rolesToAdd = request.Roles.Except(currentNormalizedRoleNames).ToList();
                var rolesToRemove = currentNormalizedRoleNames.Except(request.Roles).ToList();
                await _userManager.RemoveFromRolesAsync(staff, rolesToRemove);
                await _userManager.AddToRolesAsync(staff, rolesToAdd);

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Chỉnh sửa nhân viên thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        private ValidationNotify<string> ValidUser(IdentityResult result)
        {
            ValidationNotify<string> validation = new ValidationNotify<string>();

            foreach (var error in result.Errors)
            {
                switch (error.Code)
                {
                    case "DuplicateEmail":
                        // Xử lý lỗi email trùng lặp
                        validation.Obj = "email";
                        validation.Message = "Email đã có người dùng đăng ký.";
                        break;

                    case "DuplicateUserName":
                        // Xử lý lỗi tên người dùng trùng lặp
                        validation.Obj = "userName";
                        validation.Message = "Tên người dùng đã có người dùng đăng ký.";
                        break;

                    case "InvalidEmail":
                        // Xử lý lỗi email không hợp lệ
                        validation.Obj = "email";
                        validation.Message = "Email không hợp lệ.";
                        break;

                    case "InvalidUserName":
                        // Xử lý lỗi tên người dùng không hợp lệ
                        validation.Obj = "userName";
                        validation.Message = "Tên người dùng không hợp lệ.";
                        break;

                    case "PasswordTooShort":
                    case "PasswordRequiresNonAlphanumeric":
                    case "PasswordRequiresDigit":
                    case "PasswordRequiresLower":
                    case "PasswordRequiresUpper":
                    case "PasswordRequiresUniqueChars":
                        validation.Obj = "password";
                        validation.Message = "Mật khẩu phải từ 8 ký tự trở lên, có ít nhất 1 chữ hoa, 1 chữ thường và 1 ký tự đặc biệt.";
                        break;

                    default:
                        // Xử lý các lỗi khác nếu có
                        validation.IsSuccessed = false;
                        validation.Message = "Đã xảy ra lỗi trong quá trình đăng ký.";
                        break;
                }
            }

            return validation;
        }
    }
}
