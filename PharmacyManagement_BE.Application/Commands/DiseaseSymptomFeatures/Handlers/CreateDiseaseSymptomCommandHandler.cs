using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Handlers
{
    internal class CreateDiseaseSymptomCommandHandler : IRequestHandler<CreateDiseaseSymptomCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateDiseaseSymptomCommandHandler(IPMEntities entities, IMapper mapper)
        {
            _entities = entities;
            _mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateDiseaseSymptomCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: kiểm tra giá trị đầu 
                var symptom = await _entities.SymptomService.GetById(request.SymptomId);

                if (symptom == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Triệu chứng không tồn tại.");

                var disease = await _entities.DiseaseService.GetById(request.DiseaseId);

                if (disease == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Bệnh không tồn tại.");

                //Kiểm tra tồn tại
                var checkExit = await _entities.DiseaseSymptomService.CheckExit(request.DiseaseId, request.SymptomId);

                if (!checkExit.ValidationNotify.IsSuccessed)
                    return checkExit;

                // Chuyển đổi request sang dữ liệu
                var create = _mapper.Map<DiseaseSymptom>(request);

                // Tạo bệnh mới
                var status = _entities.DiseaseSymptomService.Create(create);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm triệu chứng liên quan thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm triệu chứng liên quan thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}