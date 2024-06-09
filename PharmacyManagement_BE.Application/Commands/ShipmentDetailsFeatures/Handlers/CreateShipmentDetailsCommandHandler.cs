using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Handlers
{
    internal class CreateShipmentDetailsCommandHandler : IRequestHandler<CreateShipmentDetailsCommandRequest, ResponseAPI<ShipmentDetailsRequest>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateShipmentDetailsCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<ShipmentDetailsRequest>> Handle(CreateShipmentDetailsCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra chi tiết đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                    return new ResponseErrorAPI<ShipmentDetailsRequest>(StatusCodes.Status404NotFound, $"Đơn hàng có mã {request.ShipmentId} không tồn tại.");

                // Kiểm tra ràng buộc
                foreach (var item in request.ShipmentDetails)
                {
                    // Kiểm tra chi tiết đơn hàng tồn tại
                    var product = await _entities.ProductService.GetById(item.ProductId);

                    if (product == null)
                        return new ResponseErrorAPI<ShipmentDetailsRequest>(StatusCodes.Status404NotFound, $"Sản phẩm có mã {item.ProductId} không tồn tại.", item);
                }

                // Thêm chi tiết đơn hàng
                var shipmentDetails = _mapper.Map<List<ShipmentDetails>>(request.ShipmentDetails);

                foreach (var item in shipmentDetails)
                {
                    item.ShipmentId = request.ShipmentId;
                }

                var result = await _entities.ShipmentDetailsService.CreateRangeShipmentDetails(shipmentDetails);

                if (!result)
                    return new ResponseErrorAPI<ShipmentDetailsRequest>(StatusCodes.Status422UnprocessableEntity, "Vui lòng kiểm tra kỹ các chi tiết đơn hàng.");

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<ShipmentDetailsRequest>(StatusCodes.Status200OK, "Thêm chi tiết các đơn hàng thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<ShipmentDetailsRequest>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
            throw new NotImplementedException();
        }
    }
}
