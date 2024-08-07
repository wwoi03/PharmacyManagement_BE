﻿using AutoMapper;
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
    internal class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateDiseaseCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //B2: kiểm tra bệnh đã tồn tại
                var checkExit = await _entities.DiseaseService.CheckExit(request.CodeDisease, request.Name);

                if (!checkExit.IsSuccessed)
                    return checkExit;

                // Chuyển đổi request sang dữ liệu
                var createDisease = _mapper.Map<Disease>(request);

                // Tạo bệnh mới
                var disease =  _entities.DiseaseService.Create(createDisease);

                //Kiểm tra trạng thái
                if (disease == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm bệnh thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK ,"Thêm loại bệnh thành công.");
            }
            catch (Exception){
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
