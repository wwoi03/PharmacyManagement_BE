using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests
{
    public class CreateCategoryCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string Name { get; set; }
        public string CodeCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public ValidationNotify<string> Valid()
        {
            if (string.IsNullOrEmpty(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên loại sản phẩm.", "name");
            if (string.IsNullOrEmpty(CodeCategory))
                return new ValidationNotifyError<string>("Vui lòng nhập mã loại sản phẩm.", "codeCategory");

            return new ValidationNotifySuccess<string>();
        } 
    }
}
