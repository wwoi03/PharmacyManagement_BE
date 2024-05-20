using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using PharmacyManagement_BE.Application.Commands.CartFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartFeatures.Handlers
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteCartCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteCartCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // B1: Kiểm giỏ hàng tồn tại
                var cart = await _entities.CartService.GetById(request.CartId);

                if (cart == null)
                    return new ResponseErrorAPI<string>("Giỏ hàng không tồn tại.");

                // B2: Xóa sản phẩm trong giỏ hàng
                var status = _entities.CartService.Delete(cart);

                if (status == false)
                    return new ResponseErrorAPI<string>("Lỗi hệ thống, vui lòng thử lại sau.");

                // B3: lưu trạng thái database
                _entities.SaveChange();
                return new ResponseSuccessAPI<string>("Xóa sản phẩm thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>("Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
