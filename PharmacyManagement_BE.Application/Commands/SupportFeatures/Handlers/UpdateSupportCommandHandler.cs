using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SupportFeatures.Handlers
{
    internal class UpdateSupportCommandHandler : IRequestHandler<UpdateSupportCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public UpdateSupportCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateSupportCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var support = await _entities.SupportService.GetById(request.Id);

                if (support == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Hỗ trợ của thuốc không tồn tại.");

                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //Gán giá trị thay đổi
                support.Name = request.Name;
                support.Description = request.Description;

                // Cập nhật lại hỗ trợ của thuốc
                var status = _entities.SupportService.Update(support);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Cập nhật hỗ trợ của thuốc thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>("Cập nhật hỗ trợ của thuốc thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }
}
