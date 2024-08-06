using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.PromotionFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.PromotionFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;
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
    internal class GetDetailsPromotionQueryHandler : IRequestHandler<GetDetailsPromotionQueryRequest, ResponseAPI<DetailsPromotionDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsPromotionQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<DetailsPromotionDTO>> Handle(GetDetailsPromotionQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.PromotionService.GetById(request.Id);

                if (validation == null)
                    return new ResponseSuccessAPI<DetailsPromotionDTO>(StatusCodes.Status404NotFound, "Hỗ trợ của thuốc không tồn tại.");

                var Promotion = _mapper.Map<DetailsPromotionDTO>(validation);

                Promotion.ProductPromotions = await _entities.PromotionService.GetRelationShip(request.Id);

                return new ResponseSuccessAPI<DetailsPromotionDTO>(StatusCodes.Status200OK, "Thông tin hỗ trợ của thuốc", Promotion);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<DetailsPromotionDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
