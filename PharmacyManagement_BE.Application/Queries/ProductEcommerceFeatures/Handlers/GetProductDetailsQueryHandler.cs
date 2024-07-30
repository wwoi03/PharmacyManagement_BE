using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Handlers
{
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<ProductDetailsEcommerceDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<ProductDetailsEcommerceDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _entities.ProductService.GetProductWithDetails(request.Id);

                //Trả về danh sách
                return new ResponseSuccessAPI<ProductDetailsEcommerceDTO>(StatusCodes.Status200OK, "Thông tin sản phẩm", product);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<ProductDetailsEcommerceDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
