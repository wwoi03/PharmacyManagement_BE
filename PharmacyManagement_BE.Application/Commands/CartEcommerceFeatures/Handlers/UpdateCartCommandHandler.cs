using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CartEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartEcommerceFeatures.Handlers
{
    internal class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public UpdateCartCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateCartCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _entities.CartService.GetById(request.CartId);
                cart.Quantity = request.Quantity;

                _entities.CartService.Update(cart);

                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
