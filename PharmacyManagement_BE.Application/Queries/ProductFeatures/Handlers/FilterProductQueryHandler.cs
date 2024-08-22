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
    internal class FilterProductQueryHandler : IRequestHandler<FilterProductQueryRequest, ResponseAPI<List<ListProductDTO>>>
    {
        private readonly IPMEntities _entities;

        public FilterProductQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ListProductDTO>>> Handle(FilterProductQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy danh sách sản phẩm
                var response = await _entities.ProductService.FilterProducts(request.ContentStr);

                return new ResponseSuccessAPI<List<ListProductDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListProductDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
