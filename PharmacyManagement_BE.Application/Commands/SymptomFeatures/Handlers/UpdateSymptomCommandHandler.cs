﻿using MediatR;
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
    public class UpdateSymptomCommandHandler : IRequestHandler<UpdateSymptomCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        private UpdateSymptomCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateSymptomCommandRequest request, CancellationToken cancellationToken)
        {
            //Kiểm tra tồn tại
            var symptom = await _entities.SymptomService.GetById(request.Id);

            if(symptom == null )
                return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Triệu chứng không tồn tại.");

            //Kiểm tra giá trị đầu vào
            var validation = request.IsValid();

            if (!validation.IsSuccessed)
                return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);
            
            
            try
            {
                //Gán giá trị thay đổi
                symptom.Name = request.Name;
                symptom.Description = request.Description;

                // Cập nhật lại triệu chứng
                var status = _entities.SymptomService.Update(symptom);

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
