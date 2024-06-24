using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests
{
    public class CreateIngredientCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string Name { get; set; }
        public string CodeIngredient { get; set; }

        public ValidationNotify<string> IsValid()
        {
            Name = CheckInput.CheckInputName(Name);
            CodeIngredient = CheckInput.CheckInputCode(CodeIngredient);

            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên thành phần.");

            if (string.IsNullOrWhiteSpace(CodeIngredient))
                return new ValidationNotifyError<string>("Vui lòng nhập mã thành phần.");

            if (!CheckInput.IsAlphaNumeric(CodeIngredient))
                return new ValidationNotifyError<string>("Mã thành phần không hợp lệ, vui lòng kiểm tra lại");

            return new ValidationNotifySuccess<string>();
        }
    }
}
