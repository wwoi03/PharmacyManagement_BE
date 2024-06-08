using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests
{
    public class SearchDiseaseCommandRequest : IRequest<ResponseAPI<List<DetailsDiseaseResponse>>>
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
