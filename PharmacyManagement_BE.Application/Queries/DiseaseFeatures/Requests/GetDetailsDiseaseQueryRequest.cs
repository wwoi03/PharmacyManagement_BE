using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests
{
    public class GetDetailsDiseaseQueryRequest : IRequest<ResponseAPI<DiseaseDTO>>
    {
        public Guid Id { get; set; }
    }
}
