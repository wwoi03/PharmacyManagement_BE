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
    internal class GetHierarchicalCategoriesQueryHandler : IRequestHandler<GetHierarchicalCategoriesQueryRequest, ResponseAPI<List<ListHierarchicalCategoryDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetHierarchicalCategoriesQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ListHierarchicalCategoryDTO>>> Handle(GetHierarchicalCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.CategoryService.GetHierarchicalCategories();

                return new ResponseSuccessAPI<List<ListHierarchicalCategoryDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListHierarchicalCategoryDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
