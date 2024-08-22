using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.SupportFeatures.Handlers
{
    internal class CreateSupportCommandHandler : IRequestHandler<CreateSupportCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateSupportCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateSupportCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra dữ liệu đầu vào
                var validation = request.IsValid();
                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                // Không kiểm tra tên hỗ trợ của thuốc vì hỗ trợ của thuốc có thể có nhiều tên trùng nhau
                var checkExit = await _entities.SupportService.CheckExit(request.CodeSupport, request.Name);

                if (!checkExit.ValidationNotify.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, checkExit.ValidationNotify);

                // Chuyển đổi request sang dữ liệu
                var createSupport = _mapper.Map<Support>(request);

                // Tạo hỗ trợ của thuốc mới
                var status = _entities.SupportService.Create(createSupport);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm hỗ trợ của thuốc thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                //Lấy Id vừa tạo
                var support = _mapper.Map<SupportDTO>(await _entities.SupportService.FindByCode(createSupport.CodeSupport));

                //Hàm khởi tạo qhe
                CreateRelationShip createRelationShip = new CreateRelationShip(_entities, _mapper);

                if (request.ProductId != null)
                {
                    await createRelationShip.CreateProductSupport(request.ProductId, support.Id, 2);
                }

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm hỗ trợ của thuốc thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        
    }
}
