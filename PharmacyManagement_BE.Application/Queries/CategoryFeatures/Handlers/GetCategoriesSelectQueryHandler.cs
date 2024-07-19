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
    internal class GetCategoriesSelectQueryHandler : IRequestHandler<GetCategoriesSelectQueryRequest, ResponseAPI<List<SelectCategoryDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCategoriesSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectCategoryDTO>>> Handle(GetCategoriesSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.CategoryService.GetCategoriesSelect();

                return new ResponseSuccessAPI<List<SelectCategoryDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectCategoryDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống vui lòng thử lại sau.");
            }
        }
    }
}
