using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductDiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductDiseaseFeatures.Handlers
{
    internal class GetDiseaseProductsQueryHandler : IRequestHandler<GetDiseaseProductsQueryRequest, ResponseAPI<List<ProductDiseaseDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDiseaseProductsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<ProductDiseaseDTO>>> Handle(GetDiseaseProductsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách bệnh
                var list = await _entities.ProductDiseaseService.GetAllByDisease(request.Id);

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<ProductDiseaseDTO>>(list);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<ProductDiseaseDTO>>(StatusCodes.Status200OK, "Danh sách quan hệ", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<ProductDiseaseDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
