using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests
{
    public class CreateStaffCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public Guid BranchId { get; set; }
        public List<string> Roles { get; set; } 

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(FullName))
                return new ValidationNotifyError<string>("Vui lòng nhập họ và tên.");
            if (string.IsNullOrWhiteSpace(UserName))
                return new ValidationNotifyError<string>("Vui lòng tên đăng nhập.");
            if (string.IsNullOrWhiteSpace(Password))
                return new ValidationNotifyError<string>("Vui lòng nhập mật khẩu.");
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
                return new ValidationNotifyError<string>("Vui lòng xác nhận mật khẩu.");
            if (!Password.Equals(ConfirmPassword))
                return new ValidationNotifyError<string>("Mật khẩu xác nhận không chính xác.");
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return new ValidationNotifyError<string>("Vui lòng nhập số điện thoại.");
            if (string.IsNullOrWhiteSpace(Email))
                return new ValidationNotifyError<string>("Vui lòng nhập email.");
            if (string.IsNullOrWhiteSpace(Gender))
                return new ValidationNotifyError<string>("Vui lòng nhập giới tính.");
            /*if (!Gender.Any(g => g.Equals(GenderType.Female) || g.Equals(GenderType.Male) || g.Equals(GenderType.Other)))
                return new ValidationNotifyError<string>("Giới tính phải thuộc (Nam, Nữ, Khác).");*/
            if (string.IsNullOrWhiteSpace(Address))
                return new ValidationNotifyError<string>("Vui lòng nhập địa chỉ.");
            if (Roles.Count == 0)
                return new ValidationNotifyError<string>("Vui lòng thêm quyền nhân viên.");

            return new ValidationNotifySuccess<string>();
        }
    }
}
