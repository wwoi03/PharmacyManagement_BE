using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseSymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseSymptomFeatures.Requests
{
    public class GetSymptomDiseasesQueryRequest : IRequest<ResponseAPI<List<DiseaseSymptomDTO>>>
    {
        public Guid Id { get; set; }
    }
}
