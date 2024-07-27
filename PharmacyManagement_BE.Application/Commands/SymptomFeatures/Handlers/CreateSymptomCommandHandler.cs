using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
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
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                // Không kiểm tra tên triệu chứng
                var checkExit = await _entities.SymptomService.CheckExit(request.CodeSymptom, request.Name);

                if (!checkExit.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, checkExit.ValidationNotify);


                // Chuyển đổi request sang dữ liệu
                var createSymptom = _mapper.Map<Symptom>(request);

                // Tạo triệu chứng mới
                var status = _entities.SymptomService.Create(createSymptom);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm triệu chứng thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                //Lấy Id vừa tạo
                var symptom = _mapper.Map<SymptomDTO>(await _entities.SymptomService.FindByCode(createSymptom.CodeSymptom));

                //Hàm khởi tạo qhe
                CreateRelationShip createRelationShip = new CreateRelationShip(_entities,_mapper);

                if (request.DiseaseId != null)
                {
                    await createRelationShip.CreateDiseaseSymptom(request.DiseaseId, symptom.Id, 2);
                }

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Thêm triệu chứng thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }
}
