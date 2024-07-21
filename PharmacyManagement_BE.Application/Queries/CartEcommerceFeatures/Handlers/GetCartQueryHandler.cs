using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CartEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CartEcommerceFeatures.Handlers
{
    internal class GetCartQueryHandler : IRequestHandler<GetCartQueryRequest, ResponseAPI<List<ItemCartDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCartQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }
        public async Task<ResponseAPI<List<ItemCartDTO>>> Handle(GetCartQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerId = await _entities.AccountService.GetAccountId();
                var response = await _entities.CartService.GetCart(customerId);

                return new ResponseSuccessAPI<List<ItemCartDTO>> (StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ItemCartDTO>> (StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
