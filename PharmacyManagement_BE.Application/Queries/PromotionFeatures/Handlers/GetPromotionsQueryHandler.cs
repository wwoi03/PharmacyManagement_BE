using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.PromotionFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.PromotionFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.PromotionFeatures.Handlers
{
    internal class GetPromotionsQueryHandler : IRequestHandler<GetPromotionsQueryRequest, ResponseAPI<List<PromotionDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetPromotionsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<PromotionDTO>>> Handle(GetPromotionsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách khuyến mãi của thuốc
                var listPromotion = await _entities.PromotionService.GetAll();

                //Gán danh sách khuyến mãi của thuốc thành response
                var response = _mapper.Map<List<PromotionDTO>>(listPromotion);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<PromotionDTO>>(StatusCodes.Status200OK, "Danh sách khuyến mãi của thuốc", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<PromotionDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}

