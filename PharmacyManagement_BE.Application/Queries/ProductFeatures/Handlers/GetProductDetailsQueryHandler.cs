using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Handlers
{
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<DetailsProductDTO>>
    {
        private readonly IPMEntities _entities;

        public GetProductDetailsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsProductDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
