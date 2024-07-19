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
    internal class GetProductSelectQueryHandler : IRequestHandler<GetProductSelectQueryRequest, ResponseAPI<List<SelectProductDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetProductSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectProductDTO>>> Handle(GetProductSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.ProductService.GetProductsSelect();

                return new ResponseSuccessAPI<List<SelectProductDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectProductDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
