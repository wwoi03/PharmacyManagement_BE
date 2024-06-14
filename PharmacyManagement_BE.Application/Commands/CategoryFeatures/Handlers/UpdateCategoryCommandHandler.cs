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
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra danh mục tồn tại
                var category = await _entities.CategoryService.GetById(request.Id);

                if (category == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Loại sản phẩm có mã {request.Id} không tồn tại.");

                // Kiểm tra danh mục trùng lặp
                var checkName = await _entities.CategoryService.GetCategoryByName(request.Name);
                if (checkName != null && !category.Name.Equals(request.Name) && checkName.Name.Equals(request.Name))
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Loại sản phẩm có tên {request.Name} đã tồn tại.");

                var checkCode = await _entities.CategoryService.GetCategoryByCode(request.CodeCategory);
                if (checkCode != null && !category.CodeCategory.Equals(request.CodeCategory) && checkCode.CodeCategory.Equals(request.CodeCategory))
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, $"Loại sản phẩm có mã {request.CodeCategory} đã tồn tại.");

                // Cập nhật loại sản phẩm
                _mapper.Map(request, category);
                var result = _entities.CategoryService.Update(category);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Hiện tại không thể cập nhật loại sản phẩm này.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Cập nhật loại sản phẩm có mã {category.CodeCategory} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
