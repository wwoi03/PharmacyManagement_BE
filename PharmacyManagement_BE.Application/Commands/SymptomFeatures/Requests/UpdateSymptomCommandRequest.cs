using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests
{
    public class UpdateSymptomCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CodeSymptom { get; set; }

        public ValidationNotify<string> IsValid()
        {
            Name = CheckInput.CheckInputName(Name);
            Description = CheckInput.CheckInputName(Description);
            CodeSymptom = CheckInput.CheckInputCode(CodeSymptom);

            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên triệu chứng.");

            if (string.IsNullOrWhiteSpace(CodeSymptom))
                return new ValidationNotifyError<string>("Vui lòng nhập mã triệu chứng.");

            if (!CheckInput.IsAlphaNumeric(CodeSymptom))
                return new ValidationNotifyError<string>("Mã triệu chứng không hợp lệ, vui lòng kiểm tra lại");

            return new ValidationNotifySuccess<string>();
        }
    }
}
