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
    internal class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteIngredientCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteIngredientCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var ingredient = await _entities.IngredientService.GetById(request.Id);

                if (ingredient == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Thành phần không tồn tại.");

                // xóa Thành phần
                var status = _entities.IngredientService.Delete(ingredient);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa thành phần thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Xóa thành phần thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
