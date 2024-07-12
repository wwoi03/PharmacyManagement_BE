using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Handlers
{
    internal class CreateProductDiseaseCommandHandler : IRequestHandler<CreateProductDiseaseCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateProductDiseaseCommandHandler(IPMEntities entities, IMapper mapper)
        {
            _entities = entities;
            _mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateProductDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
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

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm thuốc liên quan thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
