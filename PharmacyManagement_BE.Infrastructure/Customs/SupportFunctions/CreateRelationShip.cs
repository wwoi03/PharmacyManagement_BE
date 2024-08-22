using AutoMapper;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions
{
    public class createDiseaseSymptom
    {
        public Guid? DiseaseId { get; set; }
        public Guid? SymptomId { get; set; }
    }

    public class createProductDisease
    {
        public Guid? DiseaseId { get; set; }
        public Guid? ProductId { get; set; }
    }

    public class createProductSupport
    {
        public Guid? SupportId { get; set; }
        public Guid? ProductId { get; set; }
    }

    public class createProductPromotion {
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public string AdditionalInfo { get; set; }
        public int Quantity { get; set; }
    }

    public class createPromotionProgram
    {
        public Guid ProductId { get; set; }
        public Guid PromotionProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateRelationShip
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateRelationShip() { }

        public CreateRelationShip(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> CreateDiseaseSymptom(List<Guid?>? listId, Guid Id, int check)
        {
            try
            {
                foreach (var item in listId)
                {
                    createDiseaseSymptom request = new createDiseaseSymptom();
                    if(check == 1)
                    {
                        request = new createDiseaseSymptom
                        {
                            DiseaseId = Id,
                            SymptomId = item,
                        };
                    }
                    else if(check == 2)
                    {
                        request = new createDiseaseSymptom
                        {
                            DiseaseId = item,
                            SymptomId = Id,
                        };
                    }
                   

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

        public async Task<ResponseAPI<string>> CreateProductSupport(List<Guid?>? listId, Guid Id, int check)
        {
            try
            {
                foreach (var item in listId)
                {
                    createProductSupport request = new createProductSupport();
                    if (check == 1)
                    {
                        request = new createProductSupport
                        {
                            ProductId = Id,
                            SupportId = item,
                        };
                    }
                    else if (check == 2)
                    {
                        request = new createProductSupport
                        {
                            SupportId = item,
                            ProductId = Id,
                        };
                    }


                    //B1: kiểm tra giá trị đầu 
                    var support = await _entities.SupportService.GetById(request.SupportId);

                    if (support == null)
                        return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Hỗ trợ không tồn tại.");

                    var product = await _entities.DiseaseService.GetById(request.ProductId);

                    if (product == null)
                        return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại.");

                    //Kiểm tra tồn tại
                    var checkExit = await _entities.ProductSupportService.CheckExit(request.ProductId, request.SupportId);

                    if (!checkExit.ValidationNotify.IsSuccessed)
                        return checkExit;

                    // Chuyển đổi request sang dữ liệu
                    var create = _mapper.Map<ProductSupport>(request);

                    // Tạo bệnh mới
                    var status = _entities.ProductSupportService.Create(create);

                    //Kiểm tra trạng thái
                    if (status == false)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm quan hệ liên quan thất bại, vui lòng thử lại sau.");

                    //Lưu vào CSDL
                    _entities.SaveChange();

                }
                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm quan hệ liên quan thành công.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }


        public async Task<ResponseAPI<string>> CreateProductPromotion(List<ProductPromotionRequestDTO> ProductPromotions, Guid Id)
        {
            try
            {
                foreach(var ProductPromotion in ProductPromotions)
                {
                    foreach (var item in ProductPromotion.ProductId)
                    {
                        createProductPromotion request = new createProductPromotion();

                        request = new createProductPromotion
                        {
                            PromotionId = Id,
                            ProductId = item,
                            AdditionalInfo = ProductPromotion.AdditionalInfo,
                            Quantity = ProductPromotion.Quantity,
                        };

                        // Chuyển đổi request sang dữ liệu
                        var create = _mapper.Map<PromotionProduct>(request);
                        create.Id = Guid.NewGuid();

                        // Tạo bệnh mới
                        var status = _entities.PromotionProductService.Create(create);

                        //Kiểm tra trạng thái
                        if (status == false)
                            return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm quan hệ liên quan thất bại, vui lòng thử lại sau.");

                        //Tạo quan hệ mua x tặng y
                        if (ProductPromotion.promotionProgramRequest != null)
                        {
                            await CreatePromotionProgram(ProductPromotion.promotionProgramRequest, create.Id);
                        }

                        //Lưu vào CSDL
                        _entities.SaveChange();

                    }
                }
                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm quan hệ liên quan thành công.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }

        public async Task<ResponseAPI<string>> CreatePromotionProgram(List<PromotionProgramRequestDTO> promotionPrograms, Guid Id)
        {
            try
            {
                foreach(var promotionProgram in promotionPrograms)
                {
                    foreach (var item in promotionProgram.ProductId)
                    {
                        createPromotionProgram request = new createPromotionProgram();

                        request = new createPromotionProgram
                        {
                            PromotionProductId = Id,
                            ProductId = item,
                            Quantity = promotionProgram.Quantity,
                        };

                        // Chuyển đổi request sang dữ liệu
                        var create = _mapper.Map<PromotionProgram>(request);

                        // Tạo bệnh mới
                        var status = _entities.PromotionProgramService.Create(create);

                        //Kiểm tra trạng thái
                        if (status == false)
                            return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm quan hệ liên quan thất bại, vui lòng thử lại sau.");

                        //Lưu vào CSDL
                        _entities.SaveChange();

                    }
                }
                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm quan hệ liên quan thành công.");
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }

}
