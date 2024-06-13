using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests
{
    public class GetSupportsQueryRequest : IRequest<ResponseAPI<List<SupportDTO>>>
    {
    }
}
