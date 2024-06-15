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
    internal class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQueryRequest, ResponseAPI<List<IngredientDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetIngredientsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<IngredientDTO>>> Handle(GetIngredientsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách bệnh
                var listIngredient = await _entities.IngredientService.GetAll();

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<IngredientDTO>>(listIngredient);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<IngredientDTO>>(StatusCodes.Status200OK, "Danh sách thành phần", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<IngredientDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
