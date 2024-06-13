using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests
{
    public class SearchSupportQueryRequest : IRequest<ResponseAPI<List<SupportDTO>>>
    {
        public string KeyWord { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(KeyWord))
                return new ValidationNotifyError<string>("Vui lòng nhập từ khóa tìm kiếm.");
            return new ValidationNotifySuccess<string>();
        }
    }
}
