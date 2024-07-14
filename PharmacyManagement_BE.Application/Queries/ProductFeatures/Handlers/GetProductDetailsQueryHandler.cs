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
                var product = _entities.ProductService.GetById(request.ProductId);

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<DetailsProductDTO>(product);

                //Trả về danh sách
                return new ResponseSuccessAPI<DetailsProductDTO>(StatusCodes.Status200OK, "Thông tin sản phẩm", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<DetailsProductDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
