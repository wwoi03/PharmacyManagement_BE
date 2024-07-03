using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CategoryFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CategoryFeatures.Handlers
{
    internal class GetCategoriesByLevelQueryHandler : IRequestHandler<GetCategoriesByLevelQueryRequest, ResponseAPI<List<ListCategoryByLevelDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCategoriesByLevelQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ListCategoryByLevelDTO>>> Handle(GetCategoriesByLevelQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.CategoryService.GetCategoriesByLevel();

                return new ResponseSuccessAPI<List<ListCategoryByLevelDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListCategoryByLevelDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
