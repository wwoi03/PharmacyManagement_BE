using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Handlers
{
    internal class CreateProductSupportCommandHandler : IRequestHandler<CreateProductSupportCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateProductSupportCommandHandler(IPMEntities entities, IMapper mapper)
        {
            _entities = entities;
            _mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateProductSupportCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: kiểm tra giá trị đầu 
                var support = await _entities.SupportService.GetById(request.SupportId);

                if (support == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Hỗ trợ không tồn tại.");

                var product = await _entities.ProductService.GetById(request.ProductId);

                if (product == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại.");

                // Chuyển đổi request sang dữ liệu
                var create = _mapper.Map<ProductSupport>(request);

                // Tạo bệnh mới
                var status = _entities.ProductSupportService.Create(create);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm sản phẩm liên quan thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm sản phẩm liên quan thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}