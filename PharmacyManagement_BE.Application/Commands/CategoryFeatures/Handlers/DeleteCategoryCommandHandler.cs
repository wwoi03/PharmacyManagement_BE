using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CategoryFeatures.Handlers
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteCategoryCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra danh mục tồn tại
                var category = await _entities.CategoryService.GetById(request.CategoryId);

                if (category == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Loại sản phẩm có mã {request.CategoryId} không tồn tại.");

                // Xóa loại sản phẩm
                var result = _entities.CategoryService.Delete(category);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Hiện tại không thể xóa loại sản phẩm này.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Xóa loại sản phẩm có mã {category.CodeCategory} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
