using AutoMapper;
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
    internal class UpdateShipmentDetailsCommandHandler : IRequestHandler<UpdateShipmentDetailsCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public UpdateShipmentDetailsCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(UpdateShipmentDetailsCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                // Kiểm tra chi tiết đơn hàng tồn tại
                var shipmentDetails = await _entities.ShipmentDetailsService.GetById(request.ShipmentDetailsId);

                if (shipmentDetails == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Chi tiết đơn hàng không tồn tại.");

                // Kiểm tra chi tiết đơn hàng tồn tại
                var product = await _entities.ProductService.GetById(request.ProductId);

                if (product == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Sản phẩm không tồn tại không tồn tại.");

                // Kiểm tra chi tiết đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Đơn hàng không tồn tại.");

                // Cập nhật chi tiết đơn hàng
                _mapper.Map(request, shipmentDetails);
                shipmentDetails.UpdatedTime = DateTime.Now;
                var result = _entities.ShipmentDetailsService.Update(shipmentDetails);

                if (!result)
                    return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, "Hiện tại không thể cập nhật chi tiết đơn hàng này.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, $"Cập nhật tiết đơn hàng có mã {request.ShipmentDetailsId} thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
