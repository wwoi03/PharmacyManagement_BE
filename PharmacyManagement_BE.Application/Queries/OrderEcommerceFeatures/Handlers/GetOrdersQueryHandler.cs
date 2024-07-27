using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.OrderEcommerceFeatures.Request;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.OrderEcommerceFeatures.Handlers
{
    internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, ResponseAPI<List<ItemOrderDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetOrdersQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ItemOrderDTO>>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerId = await _entities.AccountService.GetAccountId();
                var response = await _entities.OrderService.GetMyOrders(customerId);

                return new ResponseSuccessAPI<List<ItemOrderDTO>> (StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ItemOrderDTO>> (StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
