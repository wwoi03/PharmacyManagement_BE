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
    internal class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public UpdateShipmentCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateShipmentCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var a = await _entities.AccountService.GetAccountId();
                // Kiểm tra đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Đơn hàng không tồn tại.");

                // Kiểm tra Nhân viên tồn tại
                var staff = await _entities.StaffService.GetById(request.StaffId);

                if (staff == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Kiểm tra Nhà cung cấp tồn tại
                var supplier = await _entities.SupplierService.GetById(request.SupplierId);

                if (supplier == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhà cung cấp không tồn tại.");

                // Kiểm tra Chi nhánh tồn tại
                var branch = await _entities.BranchService.GetById(request.BranchId);

                if (branch == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Chi nhánh không tồn tại.");

                // Cập nhật đơn hàng mới
                _mapper.Map(request, shipment);
                shipment.UpdatedTime = DateTime.Now;
                var result = _entities.ShipmentService.Update(shipment);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Cập nhật đơn hàng có mã {shipment.Id} thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
