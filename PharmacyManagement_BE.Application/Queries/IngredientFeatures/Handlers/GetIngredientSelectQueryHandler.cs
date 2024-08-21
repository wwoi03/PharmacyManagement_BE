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
    internal class GetIngredientSelectQueryHandler : IRequestHandler<GetIngredientSelectQueryRequest, ResponseAPI<List<SelectIngredientDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetIngredientSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectIngredientDTO>>> Handle(GetIngredientSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.IngredientService.GetIngredientSelect();

                return new ResponseSuccessAPI<List<SelectIngredientDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectIngredientDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
