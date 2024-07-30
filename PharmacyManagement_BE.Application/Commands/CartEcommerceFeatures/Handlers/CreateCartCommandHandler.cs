using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CartEcommerceFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartEcommerceFeatures.Handlers
{
    internal class CreateCartCommandHandler : IRequestHandler<CreateCartCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public CreateCartCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(CreateCartCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customerId = await _entities.AccountService.GetAccountId();

                var cart = new Cart
                {
                    ProductId = request.ProductId,
                    UnitId = request.UnitId,
                    Quantity = request.Quantity,
                    CustomerId = customerId,
                };

                _entities.CartService.Create(cart);

                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm thành công sản phẩm vào giỏ hàng");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
