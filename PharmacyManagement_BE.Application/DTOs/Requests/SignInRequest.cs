using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.DTOs.Requests
{
    public class SignInRequest : IRequest<ResponseAPI<SignInResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ValidationNotify<bool> IsValid()
        {
            if (string.IsNullOrEmpty(UserName))
                return new ValidationNotifySuccess<bool>("Vui lòng nhập tên đăng nhập.");
            if (string.IsNullOrEmpty(Password))
                return new ValidationNotifySuccess<bool>("Vui lòng nhập mật khẩu.");

            return new ValidationNotifySuccess<bool>();
        }
    }
}
