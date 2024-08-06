using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PromotionFeatures.Handlers
{
    internal class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommandRequest, ResponseAPI<string>>
    {

        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public DeletePromotionCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(DeletePromotionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra tồn tại
                var promotion = await _entities.PromotionService.GetById(request.Id);

                if (promotion == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Không tìm thấy khuyến mãi của thuốc.");

                //Kiểm tra nếu ngày áp dụng chưa tới thì mới được xóa
                if(promotion.StartDate < DateTime.Now)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, "Không thể xóa khuyến mãi đã được áp dụng.");

                //Xóa quan hệ của khuyến mãi nếu có 

                var delete = await _entities.PromotionService.DeleteRelationShip(request.Id);

                //Kiểm tra xóa quan hệ
                if (!delete.IsSuccessed)
                    return delete;

                //Xóa khuyến mãi của thuốc
                var deletePromotion = _entities.PromotionService.Delete(promotion);

                //Kiểm tra trạng thái xóa
                if (deletePromotion == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa khuyến mãi của thuốc thất bại, vui lòng thử lại sau.");

                // lưu vào database
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Xóa khuyến mãi của thuốc thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
