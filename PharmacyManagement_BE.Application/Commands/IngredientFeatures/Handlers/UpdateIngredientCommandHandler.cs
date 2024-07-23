using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.IngredientFeatures.Handlers
{
    internal class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommandRequest , ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public UpdateIngredientCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateIngredientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var ingredient = await _entities.IngredientService.GetById(request.Id);

                if (ingredient == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Thành phần không tồn tại.");

                //B1: kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //B2: kiểm tra thành phần đã tồn tại
                var ingredientExit = await _entities.IngredientService.CheckExit(request.CodeIngredient, request.Name, request.Id);

                if (!ingredientExit.ValidationNotify.IsSuccessed)
                    return ingredientExit;

                //Gán giá trị thay đổi
                ingredient.Name = request.Name;
                ingredient.CodeIngredient = request.CodeIngredient;

                // Cập nhật lại thành phần
                var status = _entities.IngredientService.Update(ingredient);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Cập nhật thành phần thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Cập nhật thành phần thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
