using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PromotionFeatures.Handlers
{
    internal class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommandRequest, ResponseAPI<string>>  
    {

        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public UpdatePromotionCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(UpdatePromotionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy request 
                var editPromotion = await _entities.PromotionService.GetById(request.Id);

                if (editPromotion == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Không tìm thấy khuyến mãi của thuốc.");

                //Kiểm tra nếu ngày áp dụng chưa tới thì mới được xóa
                if (editPromotion.StartDate < DateTime.Now)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, "Không thể sửa khuyến mãi đã được áp dụng.");

                //Kiểm tra dữ liệu đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                // Không kiểm tra mã khuyến mãi
                var checkExit = await _entities.PromotionService.CheckExit(request.CodePromotion);

                if (!checkExit.ValidationNotify.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, checkExit.ValidationNotify);
                

                //Update 
                editPromotion.Name = request.Name;
                editPromotion.Description = request.Description;
                editPromotion.StartDate = request.StartDate;
                editPromotion.EndDate = request.EndDate;
                editPromotion.DiscountType = request.DiscountType;
                editPromotion.DiscountValue = request.DiscountValue;
                editPromotion.CodePromotion = request.CodePromotion;

                //Chỉnh sửa quan hệ = cách xóa quan hệ cũ sau đó thêm quan hệ mới

                //Xóa quan hệ của khuyến mãi nếu có 
                var delete = await _entities.PromotionService.DeleteRelationShip(request.Id);

                //Kiểm tra xóa quan hệ, chỉ trả về khi lỗi 500
                if (!delete.IsSuccessed)
                    return delete;

                //Hàm khởi tạo qhe
                CreateRelationShip createRelationShip = new CreateRelationShip(_entities, _mapper);

                if (request.ProductPromotionRequest != null)
                {
                    await createRelationShip.CreateProductPromotion(request.ProductPromotionRequest, editPromotion.Id);
                }

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm khuyến mãi thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
