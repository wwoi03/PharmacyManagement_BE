using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseSymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Handlers
{
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<DetailsProductDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<DetailsProductDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy thông tin sản phẩm
                var product = await _entities.ProductService.GetById(request.ProductId);

                // Lấy danh sách thành phần
                var productIngredients = await _entities.ProductIngredientService.GetProductIngredientByProductId(product.Id);

                // Lấy danh sách hỗ trợ
                var productSupports = await _entities.ProductSupportService.GetProductSupportsByProductId(product.Id);

                // Lấy danh sách loại bệnh
                var productDiseases = await _entities.ProductDiseaseService.GetProductDiseasesByProductId(product.Id);

                // Lấy danh sách hình ảnh
                var images = await _entities.ProductImageService.GetProductImagesByProductId(product.Id);

                // response
                var response = _mapper.Map<DetailsProductDTO>(product);
                response.ProductIngredients = productIngredients.Select(item => item.IngredientId).ToList();
                response.ProductSupports = productSupports.Select(item => item.SupportId).ToList();
                response.ProductDiseases = productDiseases.Select(item => item.DiseaseId).ToList();
                response.Images = images.Select(item => item.Image).ToList();

                return new ResponseSuccessAPI<DetailsProductDTO>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsProductDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
