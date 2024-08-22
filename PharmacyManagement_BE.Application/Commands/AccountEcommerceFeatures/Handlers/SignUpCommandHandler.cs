using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.AccountEcommerceFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AccountEcommerceFeatures.Handlers
{
    internal class SignUpCommandHandler : IRequestHandler<SignUpCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SignUpCommandHandler(
            IPMEntities entities,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<ResponseAPI<string>> Handle(SignUpCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra thông tin yêu cầu
                var validation = request.IsValid();

                if (validation.IsSuccessed == false)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);

                // Kiểm tra người dùng tồn tại
                var userExists = await _entities.CustomerService.GetCustomerByUsername(request.UserName);

                if (userExists != null)
                {
                    validation.Obj = "default";
                    validation.Message = "Người dùng đã tồn tại.";
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                }

                // Thêm người dùng mới
                var user = new Customer
                {
                    UserName = request.UserName,
                    FullName = request.UserName,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    validation = ValidUser(result);
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                }

                // Lưu lại trạng thái Database
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Đăng ký tài khoản thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseErrorAPI<string>("Lỗi hệ thống, vui lòng thử lại sau.");
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
