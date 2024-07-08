using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests
{
    public class UpdateStaffCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
        public Guid? BranchId { get; set; }
        public List<string> Roles { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrEmpty(FullName))
                return new ValidationNotifyError<string>("Vui lòng nhập họ và tên.", "fullName");
            if (string.IsNullOrEmpty(UserName))
                return new ValidationNotifyError<string>("Vui lòng tên đăng nhập.", "userName");
            if (string.IsNullOrEmpty(Password))
                return new ValidationNotifyError<string>("Vui lòng nhập mật khẩu.", "password");
            if (string.IsNullOrEmpty(ConfirmPassword))
                return new ValidationNotifyError<string>("Vui lòng xác nhận mật khẩu.", "confirmPassword");
            if (!Password.Equals(ConfirmPassword))
                return new ValidationNotifyError<string>("Mật khẩu xác nhận không chính xác.", "confirmPassword");
            if (string.IsNullOrEmpty(PhoneNumber))
                return new ValidationNotifyError<string>("Vui lòng nhập số điện thoại.", "phoneNumber");
            if (string.IsNullOrEmpty(Email))
                return new ValidationNotifyError<string>("Vui lòng nhập email.", "email");
            if (string.IsNullOrEmpty(Gender))
                return new ValidationNotifyError<string>("Vui lòng nhập giới tính.", "gender");
            /*if (!Gender.Any(g => g.Equals(GenderType.Female) || g.Equals(GenderType.Male) || g.Equals(GenderType.Other)))
                return new ValidationNotifyError<string>("Giới tính phải thuộc (Nam, Nữ, Khác).");*/
            if (string.IsNullOrEmpty(Address))
                return new ValidationNotifyError<string>("Vui lòng nhập địa chỉ.", "address");
            if (Roles.Count == 0)
                return new ValidationNotifyError<string>("Vui lòng thêm quyền nhân viên.", "roles");

            return new ValidationNotifySuccess<string>();
        }
    }
}
