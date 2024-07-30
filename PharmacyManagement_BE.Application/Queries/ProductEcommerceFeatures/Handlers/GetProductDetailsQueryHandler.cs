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
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<ProductEcommerceDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<ProductEcommerceDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _entities.ProductService.GetProductWithDetails(request.ProductId);

                //Trả về danh sách
                return new ResponseSuccessAPI<ProductEcommerceDTO>(StatusCodes.Status200OK, "Thông tin sản phẩm", product);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<ProductEcommerceDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
