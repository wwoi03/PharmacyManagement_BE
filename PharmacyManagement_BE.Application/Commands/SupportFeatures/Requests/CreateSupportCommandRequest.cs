using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests
{
    public class CreateSupportCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên hỗ trợ của thuốc.");
            return new ValidationNotifySuccess<string>();
        }
    }
}
