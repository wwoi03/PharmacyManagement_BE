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
    internal class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQueryRequest, ResponseAPI<DetailsCategoryDTO>>
    {
        private readonly IPMEntities _entities;

        public GetCategoryDetailsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsCategoryDTO>> Handle(GetCategoryDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy chi tiết danh mục
                var response = await _entities.CategoryService.GetCategoryDetails(request.CategoryId);

                // Kiểm tra danh mục tồn tại
                if (response == null)
                    return new ResponseErrorAPI<DetailsCategoryDTO>(StatusCodes.Status409Conflict, $"Loại sản phẩm có mã {request.CategoryId} không tồn tại.");

                response.ChildrenCategories = await _entities.CategoryService.GetChildrenCategories(request.CategoryId);

                return new ResponseSuccessAPI<DetailsCategoryDTO>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsCategoryDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
