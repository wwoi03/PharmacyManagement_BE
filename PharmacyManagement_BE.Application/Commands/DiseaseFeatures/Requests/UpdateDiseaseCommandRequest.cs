using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests
{
    public class UpdateDiseaseCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên bệnh.");
            if (string.IsNullOrWhiteSpace(Description))
                return new ValidationNotifyError<string>("Vui lòng nhập mô tả về bệnh.");
            return new ValidationNotifySuccess<string>();
        }
    }
}
