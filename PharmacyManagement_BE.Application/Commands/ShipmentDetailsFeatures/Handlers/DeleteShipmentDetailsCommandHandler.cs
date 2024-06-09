using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Handlers
{
    internal class DeleteShipmentDetailsCommandHandler : IRequestHandler<DeleteShipmentDetailsCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public DeleteShipmentDetailsCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteShipmentDetailsCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra chi tiết đơn hàng tồn tại
                var shipmentDetails = await _entities.ShipmentDetailsService.GetById(request.ShipmentDetailsId);

                if (shipmentDetails == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Chi tiết đơn hàng không tồn tại.");

                // Xóa đơn hàng
                var result = _entities.ShipmentDetailsService.Delete(shipmentDetails);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, "Hiện tại không thể xóa chi tiết đơn hàng này.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Xóa chi tiết đơn hàng có mã {request.ShipmentDetailsId} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
