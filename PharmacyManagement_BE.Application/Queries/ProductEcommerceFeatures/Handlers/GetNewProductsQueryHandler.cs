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
    internal class GetNewProductsQueryHandler : IRequestHandler<GetNewProductsQueryRequest, ResponseAPI<List<ItemProductDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetNewProductsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ItemProductDTO>>> Handle(GetNewProductsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currentDate = DateTime.Now;
                var response = await _entities.ProductService.GetNewProducts();

                return new ResponseSuccessAPI<List<ItemProductDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ItemProductDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
