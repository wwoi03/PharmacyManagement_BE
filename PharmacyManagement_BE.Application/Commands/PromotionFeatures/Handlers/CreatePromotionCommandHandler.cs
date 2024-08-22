using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
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
    internal class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreatePromotionCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreatePromotionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra dữ liệu đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                // Không kiểm tra mã khuyến mãi
                var checkExit = await _entities.PromotionService.CheckExit(request.CodePromotion);

                if (!checkExit.ValidationNotify.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, checkExit.ValidationNotify);

                // Chuyển đổi request sang dữ liệu
                var createPromotion = _mapper.Map<Promotion>(request);
                createPromotion.Id = Guid.NewGuid();
                createPromotion.DiscountType = (PromotionType)Enum.Parse(typeof(PromotionType), request.DiscountType);

                // Tạo khuyến mãi ban đầu
                var status = _entities.PromotionService.Create(createPromotion);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm khuyến mãi thất bại, vui lòng thử lại sau.");

                //*************************************************************** Tạo PromotionProduct

                //Hàm khởi tạo qhe
                CreateRelationShip createRelationShip = new CreateRelationShip(_entities, _mapper);

                if (request.ProductPromotionRequest != null)
                {
                    await createRelationShip.CreateProductPromotion(request.ProductPromotionRequest, createPromotion.Id);
                }

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm khuyến mãi thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
