using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Handlers
{
    internal class DeleteShipmentCommandHandler : IRequestHandler<DeleteShipmentCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteShipmentCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteShipmentCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Đơn hàng không tồn tại.");

                // Xóa tất cả chi tiết của đơn hàng
                var shipmentDetails = await _entities.ShipmentService.GetShipmentDetailsByShipment(request.ShipmentId);

                if (shipmentDetails.Count > 0)
                {
                    var deleteShipmentDetailsResult = await _entities.ShipmentDetailsService.RemoveRangeShipmentDetails(shipmentDetails);
                    if (!deleteShipmentDetailsResult)
                        return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
                }

                // Xóa đơn nhập hàng
                var deleteShipmentResult = _entities.ShipmentService.Delete(shipment);
                if (!deleteShipmentResult)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Xóa đơn hàng có mã {request.ShipmentId} thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
