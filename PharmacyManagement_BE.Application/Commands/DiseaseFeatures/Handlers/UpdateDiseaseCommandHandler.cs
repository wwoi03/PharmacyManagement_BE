using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Handlers
{
    internal class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommandRequest, ResponseAPI<string>>
    {

        private readonly IPMEntities _entities;

        public UpdateDiseaseCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            //Kiểm tra tồn tại
            var disease = await _entities.DiseaseService.GetById(request.Id);

            if(disease == null)
                return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Bệnh không tồn tại.");

            //B1: kiểm tra giá trị đầu vào
            var validation = request.IsValid();

            if (!validation.IsSuccessed)
                return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

            //B2: kiểm tra bệnh đã tồn tại
            var diseaseExit = await _entities.DiseaseService.CheckExit(request.Name, request.Description);

            if (diseaseExit)
                return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Bệnh đã tồn tại.");

            try
            {
                //Gán giá trị thay đổi
                disease.Name = request.Name;
                disease.Description = request.Description;

                // Cập nhật lại bệnh
                var status = _entities.DiseaseService.Update(disease);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Cập nhật bệnh thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>("Cập nhật loại bệnh thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Lỗi hệ thống.");
            }
        }
    }
}
