using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;

namespace PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests
{
    public class UpdateIngredientCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
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
