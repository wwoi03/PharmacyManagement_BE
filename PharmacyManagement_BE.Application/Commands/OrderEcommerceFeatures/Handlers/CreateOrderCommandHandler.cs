using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Handlers
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Valid
                var validation = request.Valid();

                if (!validation.IsSuccessed)
                {
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);
                }

                // Lấy Id Khách hàng
                var customerId = await _entities.AccountService.GetAccountId();

                // Kiểm tra thông tin nhân viên nếu có
                if (request.StaffId != null)
                {
                    var staff = await _entities.StaffService.GetById(request.StaffId);

                    if (staff == null)
                    {
                        validation.Message = $"Nhân viên có mã {request.StaffId} không tồn tại.";
                        validation.Obj = "staffId";
                        return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);
                    }
                }

                // Kiểm tra thông tin chi nhánh nếu có
                if (request.BranchId != null)
                {
                    var branch = await _entities.BranchService.GetById(request.BranchId);

                    if (branch == null)
                    {
                        validation.Message = $"Chi nhánh có mã {request.BranchId} không tồn tại.";
                        validation.Obj = "branchId";
                        return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);
                    }
                }

                // Kiểm tra voucher


                // Tính tiền giảm giá


                // Tạo đơn hàng
                var order = _mapper.Map<Order>(request);
                order.Id = Guid.NewGuid();
                var orderCreateResult = _entities.OrderService.Create(order);

                if (!orderCreateResult)
                {
                    validation.Message = $"Có lỗi xảy ra trong quá trình đặt hàng, vui lòng thử lại sau.";
                    validation.Obj = "default";
                    return new ResponseSuccessAPI<string>(StatusCodes.Status409Conflict, validation);
                }

                // Tạo chi tiết đơn hàng
                decimal totalPrice = 0;

                foreach (var item in request.Products)
                {
                    var shipmentDetailsUnit = await _entities.ShipmentDetailsUnitService.GetShipmentDetailsUnit(item.ShipmentDetailsId, item.UnitId);

                    var orderDetails = new OrderDetails
                    {
                        ShipmentDetailsId = item.ShipmentDetailsId,
                        UnitId = item.UnitId,
                        Quantity = item.Quantity,
                        PricePerUnit = shipmentDetailsUnit.SalePrice,
                        TotalPrice = shipmentDetailsUnit.SalePrice * item.Quantity,
                        Status = null
                    };

                    totalPrice += orderDetails.TotalPrice;

                    _entities.OrderDetailsService.Create(orderDetails);
                }

                // Tạo cổng thanh toán
                var paymentMethod = await _entities.PaymentMethodService.GetById(request.PaymentMethodId);

                if (paymentMethod != null)
                {
                    if (paymentMethod.Name.Equals("VnPay"))
                    {
                        var paymentInfor = new PaymentInformationDTO
                        {
                            OrderType = "Đặt hàng",
                            Amount = totalPrice,
                            OrderDescription = order.Note != null ? order.Note : "",
                            Name = order.OrdererName,

                        };

                        var paymentUrl = await _entities.VnPayService.CreatePaymentUrl(paymentInfor, request.Context);

                        return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, null, paymentUrl);
                    }
                }

                // SaveChange
                //_entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Đặt hàng thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
