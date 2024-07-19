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
    internal class CreateShipmentDetailsCommandHandler : IRequestHandler<CreateShipmentDetailsCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateShipmentDetailsCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateShipmentDetailsCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra thông tin
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);

                // Kiểm tra chi tiết đơn hàng tồn tại
                var shipment = await _entities.ShipmentService.GetById(request.ShipmentId);

                if (shipment == null)
                {
                    validation.Obj = "shipmentId";
                    validation.Message = $"Đơn hàng có mã {request.ShipmentId} không tồn tại.";
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, validation);
                }

                // Kiểm tra sản phẩm tồn tại
                var product = await _entities.ProductService.GetById(request.ProductId);

                if (product == null)
                {
                    validation.Obj = "productId";
                    validation.Message = $"Sản phẩm có mã {request.ProductId} không tồn tại.";
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, validation);
                }

                // Thêm chi tiết đơn hàng
                var shipmentDetails = _mapper.Map<ShipmentDetails>(request);
                shipmentDetails.Id = Guid.NewGuid();
                var result = _entities.ShipmentDetailsService.Create(shipmentDetails);

                if (!result)
                {
                    validation.Obj = "default";
                    validation.Message = "Vui lòng kiểm tra kỹ các chi tiết đơn hàng.";
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, validation);
                }

                // Thêm giá bán
                if (request.ShipmentDetailsUnits != null && request.ShipmentDetailsUnits.Count > 0)
                {
                    foreach (var item in request.ShipmentDetailsUnits)
                    {
                        var unitResult = await _entities.UnitService.GetUnitByNameOrCode(item.UnitName, item.CodeUnit);
                        var unitId = Guid.NewGuid();

                        if (item.Level == 1)
                        {
                            request.UnitId = unitId;
                        }

                        if (unitResult != null)
                        {
                            unitId = unitResult.Id;
                            
                        } 
                        else
                        {
                            var unit = new Domain.Entities.Unit
                            {
                                Id = unitId,
                                Name = item.UnitName,
                                NameDetails = item.CodeUnit,
                                UnitType = "PM_UNIT_PRODUCT"
                            };

                            _entities.UnitService.Create(unit);
                        }

                        var shipmentDetailsUnit = new ShipmentDetailsUnit
                        {
                            UnitId = unitId,
                            ShipmentDetailsId = shipmentDetails.Id,
                            Level = item.Level,
                            SalePrice = item.SalePrice,
                            UnitCount = item.UnitCount,
                        };

                        _entities.ShipmentDetailsUnitService.Create(shipmentDetailsUnit);
                    }
                }

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm chi tiết các đơn hàng thành công");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
