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
    public class GetDetailsSupportQueryRequest : IRequest<ResponseAPI<SupportDTO>>
    {
        public Guid Id { get; set; }
    }
}

