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
                // Valid
                var validation = request.Valid();

                // Kiểm tra danh mục tồn tại
                var categoryExists = await _entities.CategoryService.GetCategoryByNameOrCode(request.Name, request.CodeCategory);

                if (categoryExists != null)
                {
                    if (categoryExists.Name.Equals(request.Name))
                    {
                        validation.Obj = "name";
                        validation.Message = "Tên loại sản phẩm đã tồn tại.";
                    }
                    else if (categoryExists.CodeCategory.Equals(request.CodeCategory))
                    {
                        validation.Obj = "codeCategory";
                        validation.Message = "Mã loại sản phẩm đã tồn tại.";
                    }
                    
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);
                }

                // Thêm loại sản phẩm
                var category = _mapper.Map<Category>(request);
                var result = _entities.CategoryService.Create(category);

                if (!result)
                {
                    validation.Obj = "default";
                    validation.Message = "Hiện tại không thể thêm loại sản phẩm này.";
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, validation);
                }

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
