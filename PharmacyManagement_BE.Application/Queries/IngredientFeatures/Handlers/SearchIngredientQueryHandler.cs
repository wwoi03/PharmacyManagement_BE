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
    internal class SearchIngredientQueryHandler : IRequestHandler<SearchIngredientQueryRequest, ResponseAPI<List<IngredientDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchIngredientQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<IngredientDTO>>> Handle(SearchIngredientQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<List<IngredientDTO>>(StatusCodes.Status400BadRequest, validation.Message);

                // Tìm kiếm bệnh theo tên gần đúng
                var listIngredient = await _entities.IngredientService.Search(request.KeyWord, cancellationToken);

                //Gán giá trị response
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
