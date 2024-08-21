using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductPredictionFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductPredictionFeatures.Handlers
{
    internal class GetSimilarProductsQueryHandler : IRequestHandler<GetSimilarProductsQueryRequest, ResponseAPI<List<ItemProductDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetSimilarProductsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ItemProductDTO>>> Handle(GetSimilarProductsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerId = await _entities.AccountService.GetAccountId();

                var response = await _entities.PredictionService.GetSimilarProducts(customerId);

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
