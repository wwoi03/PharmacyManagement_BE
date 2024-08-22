using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SymptomFeatures.Handlers
{
    internal class DeleteSymptomCommandHandler : IRequestHandler<DeleteSymptomCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteSymptomCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteSymptomCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra tồn tại
                var symptom = await _entities.SymptomService.GetById(request.Id);

                if (symptom == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Không tìm thấy triệu chứng.");

                //Xóa triệu chứng
                var deleteSymptom = _entities.SymptomService.Delete(symptom);

                //Kiểm tra trạng thái xóa
                if (deleteSymptom == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa triệu chứng thất bại, vui lòng thử lại sau.");

                // lưu vào database
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Xóa triệu chứng thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
