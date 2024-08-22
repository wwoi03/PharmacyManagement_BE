using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Handlers
{
    internal class DeleteProductSupportCommandHandler : IRequestHandler<DeleteProductSupportCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteProductSupportCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }
        public async Task<ResponseAPI<string>> Handle(DeleteProductSupportCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra tồn tại
                var productSupport = await _entities.ProductSupportService.GetProductSupport(request.SupportId, request.ProductId);

                if (productSupport == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Quan hệ không tồn tại.");

                var status = _entities.ProductSupportService.Delete(productSupport);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa quan hệ thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Xóa quan hệ thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
