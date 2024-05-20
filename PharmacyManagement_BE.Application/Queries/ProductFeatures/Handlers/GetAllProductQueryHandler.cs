using MediatR;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Handlers
{
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, ResponseAPI<AllProductQueryResponse>>
    {
        private readonly IPMEntities _entities;

        public GetAllProductQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<AllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _entities.ProductService.GetAll();

            throw new NotImplementedException();
        }
    }
}
