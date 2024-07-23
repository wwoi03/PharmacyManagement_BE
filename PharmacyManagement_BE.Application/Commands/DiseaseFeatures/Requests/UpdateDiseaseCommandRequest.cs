using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
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
        public string CodeDisease { get; set; }

        public ValidationNotify<string> IsValid()
        {
            Name = CheckInput.CheckInputName(Name);
            Description = CheckInput.CheckInputName(Description);
            CodeDisease = CheckInput.CheckInputCode(CodeDisease);

            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên bệnh.", "name");

            if (string.IsNullOrWhiteSpace(CodeDisease))
                return new ValidationNotifyError<string>("Vui lòng nhập mã bệnh.", "codeDisease");

            if (string.IsNullOrWhiteSpace(Description))
                return new ValidationNotifyError<string>("Vui lòng nhập mô tả bệnh.", "description");

            if (!CheckInput.IsAlphaNumeric(CodeDisease))
                return new ValidationNotifyError<string>("Mã bệnh không hợp lệ, vui lòng kiểm tra lại", "codeDisease");

            return new ValidationNotifySuccess<string>();
        }
    }
}
