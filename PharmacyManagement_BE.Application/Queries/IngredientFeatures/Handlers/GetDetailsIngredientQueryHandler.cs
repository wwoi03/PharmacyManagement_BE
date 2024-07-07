using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.IngredientFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.IngredientDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.IngredientFeatures.Handlers
{
    internal class GetDetailsIngredientQueryHandler : IRequestHandler<GetDetailsIngredientQueryRequest, ResponseAPI<IngredientDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsIngredientQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<IngredientDTO>> Handle(GetDetailsIngredientQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.IngredientService.GetById(request.Id);

                if (validation == null)
                    return new ResponseErrorAPI<IngredientDTO>(StatusCodes.Status404NotFound, "Thành phần không tồn tại.");

                var ingredient = _mapper.Map<IngredientDTO>(validation);

                return new ResponseSuccessAPI<IngredientDTO>(StatusCodes.Status200OK, "Thông tin thành phần", ingredient);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<IngredientDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
