﻿using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests
{
    public class GetDiseasesQueryRequest : IRequest<ResponseAPI<List<DiseaseDTO>>>
    {
    }
}
