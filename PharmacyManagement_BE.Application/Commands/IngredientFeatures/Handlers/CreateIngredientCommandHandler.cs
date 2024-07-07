using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.IngredientFeatures.Handlers
{
    internal class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateIngredientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //B2: kiểm tra thành phần đã tồn tại
                var checkExit = await _entities.IngredientService.CheckExit(request.CodeIngredient, request.Name);

                if (!checkExit.IsSuccessed)
                    return checkExit;

                // Chuyển đổi request sang dữ liệu
                var create = _mapper.Map<Ingredient>(request);

                // Tạo thành phần mới
                var ingredient = _entities.IngredientService.Create(create);

                //Kiểm tra trạng thái
                if (ingredient == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm thành phần thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Thêm loại thành phần thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
