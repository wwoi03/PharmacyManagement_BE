using MediatR;
using PharmacyManagement_BE.Application.Queries.FilterEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.FilterEcommerceFeatures.Handlers
{
    internal class FilterQueryHandler : IRequestHandler<FilterQueryRequest, ResponseAPI<List<FilterProductDTO>>>
    {
        private readonly IPMEntities _entities;

        public FilterQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }
        public Task<ResponseAPI<List<FilterProductDTO>>> Handle(FilterQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
