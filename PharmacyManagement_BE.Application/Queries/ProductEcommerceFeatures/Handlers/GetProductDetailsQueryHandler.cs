using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Handlers
{
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<DetailsProductEcommerceDTO>>
    {
        private readonly IPMEntities _entities;

        public GetProductDetailsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsProductEcommerceDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.ProductService.GetProductDetailsEmcommerce(request.ProductId);

                return new ResponseSuccessAPI<DetailsProductEcommerceDTO>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsProductEcommerceDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
