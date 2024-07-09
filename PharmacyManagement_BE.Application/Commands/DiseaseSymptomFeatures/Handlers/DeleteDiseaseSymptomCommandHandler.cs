using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Handlers
{
    internal class DeleteDiseaseSymptomCommandHandler : IRequestHandler<DeleteDiseaseSymptomCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteDiseaseSymptomCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }
        public async Task<ResponseAPI<string>> Handle(DeleteDiseaseSymptomCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra tồn tại
                var diseaseSymptom = await _entities.DiseaseSymptomService.GetDiseaseSymptom(request.SymptomId, request.DiseaseId);

                if (diseaseSymptom == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Quan hệ không tồn tại.");

                var status = _entities.DiseaseSymptomService.Delete(diseaseSymptom);

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
