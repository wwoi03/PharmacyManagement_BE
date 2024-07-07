using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CartFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Handlers
{
    internal class DeleteDiseaseCommandHandler : IRequestHandler<DeleteDiseaseCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteDiseaseCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var disease = await _entities.DiseaseService.GetById(request.Id);

                if (disease == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Bệnh không tồn tại.");

                // Cập nhật lại bệnh
                var status = _entities.DiseaseService.Delete(disease);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa bệnh thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Xóa loại bệnh thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
