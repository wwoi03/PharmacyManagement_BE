using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CategoryFeatures.Handlers
{
    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra danh mục tồn tại
                var categoryExists = await _entities.CategoryService.GetCategoryByNameOrCode(request.Name, request.CodeCategory);

                if (categoryExists != null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, "Loại sản phẩm đã tồn tại.");

                // Thêm loại sản phẩm
                var category = _mapper.Map<Category>(request);
                var result = _entities.CategoryService.Create(category);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Hiện tại không thể thêm loại sản phẩm này.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Thêm mới loại sản phẩm có mã {category.CodeCategory} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
