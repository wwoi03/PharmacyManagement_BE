using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SupportFeatures.Handlers
{
    internal class DeleteSupportCommandHandler : IRequestHandler<DeleteSupportCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteSupportCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteSupportCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra tồn tại
                var support = await _entities.SupportService.GetById(request.Id);

                if (support == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Không tìm thấy hỗ trợ của thuốc.");

                //Xóa hỗ trợ của thuốc
                var deleteSupport = _entities.SupportService.Delete(support);

                //Kiểm tra trạng thái xóa
                if (!deleteSupport == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa hỗ trợ của thuốc thất bại, vui lòng thử lại sau.");

                // lưu vào database
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>("Xóa hỗ trợ của thuốc thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        
    }
}
