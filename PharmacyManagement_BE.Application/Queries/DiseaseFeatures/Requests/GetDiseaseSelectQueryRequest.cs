﻿using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests
{
    public class GetDiseaseSelectQueryRequest : IRequest<ResponseAPI<List<SelectDiseaseDTO>>>
    {
    }
}