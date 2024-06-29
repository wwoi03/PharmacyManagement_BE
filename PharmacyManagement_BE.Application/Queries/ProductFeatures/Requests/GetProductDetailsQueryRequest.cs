﻿using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests
{
    public class GetProductDetailsQueryRequest : IRequest<ResponseAPI<DetailsProductDTO>>
    {
        public Guid ProductId { get; set; }
    }
}
