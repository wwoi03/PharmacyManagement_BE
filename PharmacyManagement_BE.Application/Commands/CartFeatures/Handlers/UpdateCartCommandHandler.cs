using MediatR;
using PharmacyManagement_BE.Application.Commands.CartFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartFeatures.Handlers
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommandRequest, ResponseAPI<string>>
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
                // B1: Kiểm giỏ hàng tồn tại
                var cart = await _entities.CartService.GetById(request.CartId);

                if (cart == null)
                    return new ResponseErrorAPI<string>("Giỏ hàng không tồn tại.");

                //B2: Cập nhật sản phẩm
                var status = _entities.CartService.Update(cart);

                // Kiểm tra trạng thái xóa sản phẩm
                if (status == false)
                    return new ResponseErrorAPI<string>("Lỗi hệ thống, vui lòng thử lại sau.");

                //B3: Lưu vào database
                _entities.SaveChange();
                return new ResponseSuccessAPI<string>("Cập nhật thành công");

            }
            catch (Exception) {
                return new ResponseErrorAPI<string>("Lỗi hệ thống, vui lòng thử lại sau.");
            }

        }
    }
}
