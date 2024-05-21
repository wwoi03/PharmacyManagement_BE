using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.UserFeatures.Requests
{
    public class CreateUserCommandRequest : IRequest<ResponseAPI<SignUpCommandResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PIN { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrEmpty(UserName))
                return new ValidationNotifyError<string>("Vui lòng nhập tên đăng nhập.");
            if (string.IsNullOrEmpty(Password))
                return new ValidationNotifyError<string>("Vui lòng nhập mật khẩu.");
            if (string.IsNullOrEmpty(ConfirmPassword))
                return new ValidationNotifyError<string>("Vui lòng xác nhận mật khẩu.");
            if (Password.Equals(ConfirmPassword) == false)
                return new ValidationNotifyError<string>("Mật khẩu xác nhận không chính xác.");

            return new ValidationNotifySuccess<string>();
        }
    }
}
