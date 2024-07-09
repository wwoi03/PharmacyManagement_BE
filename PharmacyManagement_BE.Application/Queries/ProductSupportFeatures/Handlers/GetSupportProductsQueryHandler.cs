using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductSupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductSupportFeatures.Handlers
{
    internal class GetSupportProductsQueryHandler : IRequestHandler<GetSupportProductsQueryRequest, ResponseAPI<List<ProductSupportDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetSupportProductsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<ProductSupportDTO>>> Handle(GetSupportProductsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách 
                var list = await _entities.ProductSupportService.GetAllBySupport(request.Id);

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<ProductSupportDTO>>(list);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<ProductSupportDTO>>(StatusCodes.Status200OK, "Danh sách quan hệ", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<ProductSupportDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
