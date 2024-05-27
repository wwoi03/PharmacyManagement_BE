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
    public class CreateStaffCommandRequest : IRequest<ResponseAPI<string>>
    {
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
        public List<Guid> Roles { get; set; } 

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(Password))
                return new ValidationNotifyError<string>("Vui lòng nhập mật khẩu.");
            if (string.IsNullOrWhiteSpace(Password))
                return new ValidationNotifyError<string>("Vui lòng nhập mật khẩu.");

            return new ValidationNotifySuccess<string>();
        }
    }
}
