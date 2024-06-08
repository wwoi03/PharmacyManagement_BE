using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Handlers
{
    internal class GetShipmentDetailsByShipmentQueryHandler : IRequestHandler<GetShipmentDetailsByShipmentQueryRequest, ResponseAPI<List<ListShipmentDetailsDTOs>>>
    {
        private readonly IPMEntities _entities;

        public GetShipmentDetailsByShipmentQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ListShipmentDetailsDTOs>>> Handle(GetShipmentDetailsByShipmentQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                    return new ResponseErrorAPI<List<ListShipmentDetailsDTOs>>(StatusCodes.Status404NotFound, "Đơn hàng không tồn tại.");

                // Lấy danh sách chi tiết đơn hàng
                var response = await _entities.ShipmentDetailsService.GetShipmentDetailsByShipment(request.ShipmentId);

                return new ResponseSuccessAPI<List<ListShipmentDetailsDTOs>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListShipmentDetailsDTOs>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
