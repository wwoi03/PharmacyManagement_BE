using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Handlers
{
    internal class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateShipmentCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateShipmentCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra Nhà cung cấp tồn tại
                var supplier = await _entities.SupplierService.GetById(request.SupplierId);

                if (supplier == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhà cung cấp không tồn tại.");

                // Cập nhật đơn hàng mới
                Shipment shipment = new Shipment();
                _mapper.Map(request, shipment);
                shipment.Id = Guid.NewGuid();
                shipment.UpdatedTime = DateTime.Now;
                shipment.CreatedTime = DateTime.Now;
                shipment.BranchId = await _entities.AccountService.GetBranchId();
                shipment.StaffId = await _entities.AccountService.GetAccountId();

                var result = _entities.ShipmentService.Create(shipment);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Thêm mới đơn hàng có mã {shipment.Id} thành công", shipment.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
