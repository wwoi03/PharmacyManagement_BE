using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
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
    class createDiseaseSymptom { 
        public Guid? DiseaseId { get; set; }
        public Guid? SymptomId { get; set; }
    }

    class createProductDisease
    {
        public Guid? DiseaseId { get; set; }
        public Guid? ProductId { get; set; }
    }

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
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //B2: kiểm tra bệnh đã tồn tại
                var checkExit = await _entities.DiseaseService.CheckExit(request.CodeDisease, request.Name);

                if (!checkExit.ValidationNotify.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, checkExit.ValidationNotify);

                //Chuyển đổi request sang dữ liệu
                var createDisease = _mapper.Map<Disease>(request);
                createDisease.CreatedTime = DateTime.Now;

                // Tạo bệnh mới
                var status =  _entities.DiseaseService.Create(createDisease);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm bệnh thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                //Lấy Id vừa tạo
                var disease = _mapper.Map <DiseaseDTO>(await _entities.DiseaseService.FindByCode(createDisease.CodeDisease));


                if (request.ProductId != null)
                {
                    await CreateProductDisease(request.ProductId, disease.Id);
                }
                if (request.SymptomId != null)
                {
                    await CreateDiseaseSymptom(request.SymptomId, disease.Id);
                }

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK ,"Thêm loại bệnh thành công.");
            }
            catch (Exception){
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        public async Task<ResponseAPI<string>> CreateDiseaseSymptom(List<Guid?>? SymptomId, Guid DiseaseId)
        {
            try
            {
                foreach (var item in SymptomId)
                {
                    
                    var request = new createDiseaseSymptom()
                    {
                        DiseaseId = DiseaseId,
                        SymptomId = item,
                    };

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

                }
                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm triệu chứng liên quan thành công.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }

        public async Task<ResponseAPI<string>> CreateProductDisease(List<Guid?>? ProductId, Guid DiseaseId)
        {
            try
            {
                foreach (var item in ProductId)
                {
                    var request = new createProductDisease()
                    {
                        DiseaseId = DiseaseId,
                        ProductId = item,
                    };

                    //B1: kiểm tra giá trị đầu 
                    var product = await _entities.ProductService.GetById(request.ProductId);

                    if (product == null)
                        return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Thuốc không tồn tại.");

                    var disease = await _entities.DiseaseService.GetById(request.DiseaseId);

                    if (disease == null)
                        return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Bệnh không tồn tại.");

                    //Kiểm tra tồn tại
                    var checkExit = await _entities.ProductDiseaseService.CheckExit(request.ProductId, request.DiseaseId);

                    if (!checkExit.ValidationNotify.IsSuccessed)
                        return checkExit;

                    // Chuyển đổi request sang dữ liệu
                    var create = _mapper.Map<ProductDisease>(request);

                    // Tạo bệnh mới
                    var status = _entities.ProductDiseaseService.Create(create);

                    //Kiểm tra trạng thái
                    if (status == false)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm thuốc liên quan thất bại, vui lòng thử lại sau.");

                    //Lưu vào CSDL
                    _entities.SaveChange();

                }
                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm thuốc liên quan thành công.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }
}
