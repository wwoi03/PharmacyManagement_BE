using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.CategoryFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CategoryFeatures.Handlers
{
    internal class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, ResponseAPI<List<CategoryResponse>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<CategoryResponse>>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy danh sách loại sản phẩm
                var categories = await _entities.CategoryService.GetAll();

                // Response
                var response = categories
                    .Where(c => c.ParentCategoryId != null)
                    .Select(async c => new CategoryResponse()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentCategory = await GetParentCategory(c.ParentCategoryId),
                    });

                return new ResponseSuccessAPI<List<CategoryResponse>>(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<CategoryResponse>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        private async Task<CategoryResponse> GetParentCategory(Guid? parentId)
        {
            if (parentId != null)
            {
                var category = await _entities.CategoryService.GetById(parentId);
                var parentCategory = _mapper.Map<CategoryResponse>(category);

                if (category.ParentCategoryId != null)
                {
                    parentCategory.ParentCategory = await GetParentCategory(category.ParentCategoryId);
                }
            }

            return null;
        }
    }
}
