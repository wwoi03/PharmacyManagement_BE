﻿using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.IngredientDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.IngredientFeatures.Requests
{
    public class GetDetailsIngredientQueryRequest : IRequest<ResponseAPI<IngredientDTO>>
    {
        public Guid Id { get; set; }
    }
}
