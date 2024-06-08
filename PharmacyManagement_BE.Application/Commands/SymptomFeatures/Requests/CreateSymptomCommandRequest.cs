using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests
{
    public class CreateSymptomCommandRequest : IRequest<ResponseAPI<string>>
    {
        
        public string Name { get; set; }
        //Triệu chứng không nhất thiết phải có mô tả
        public string? Description { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return new ValidationNotifyError<string>("Vui lòng nhập tên triệu chứng.");
            return new ValidationNotifySuccess<string>();
        }
    }
}
