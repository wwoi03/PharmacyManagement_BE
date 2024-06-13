using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SymptomFeatures.Requests
{
    public class GetSymptomsQueryRequest : IRequest<ResponseAPI<List<SymptomDTO>>>
    {
    }
}
