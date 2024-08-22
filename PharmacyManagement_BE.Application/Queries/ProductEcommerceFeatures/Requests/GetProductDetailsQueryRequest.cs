﻿using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Requests
{
    public class GetProductDetailsQueryRequest : IRequest<ResponseAPI<DetailsProductEcommerceDTO>>
    {
        public Guid ProductId { get; set; }
    }
}
