﻿using AutoMapper;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PharmacyManagement_BE.Application.Commands.SymptomFeatures.Handlers
{
    internal class CreateSymptomCommandHandler : IRequestHandler<CreateSymptomCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateSymptomCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateSymptomCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra dữ liệu đầu vào
                var validation = request.IsValid();
                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                // Không kiểm tra tên triệu chứng vì triệu chứng có thể có nhiều tên trùng nhau, kể cả trên 1 loại bệnh: (đau đầu: cảm giác, đau đầu: căng thẳng)
                var checkExit = await _entities.SymptomService.CheckExit(request.Name, request.Description);

                if (checkExit)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Triệu chứng đã tồn tại.");

                // Chuyển đổi request sang dữ liệu
                var createSymptom = _mapper.Map<Symptom>(request);

                // Tạo triệu chứngmới
                var symptom = _entities.SymptomService.Create(createSymptom);

                //Kiểm tra trạng thái
                if (symptom == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm triệu chứng thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>("Thêm triệu chứng thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }
}
