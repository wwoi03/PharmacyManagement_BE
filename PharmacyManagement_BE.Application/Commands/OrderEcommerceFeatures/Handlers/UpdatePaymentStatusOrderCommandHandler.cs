using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Handlers
{
    internal class UpdatePaymentStatusOrderCommandHandler : IRequestHandler<UpdatePaymentStatusOrderCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public UpdatePaymentStatusOrderCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(UpdatePaymentStatusOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // cập nhật trạng thái thanh toán của đơn hàng
                var order = await _entities.OrderService.GetOrderByCode(request.PaymentResponse.OrderId);
                order.PaymentStatus = PaymentStatus.PaymentUnpaid.ToString(); ;
                order.PaymentAmount = 0m;

                var isPaymentSuccess = request.PaymentResponse.Success;

                if (isPaymentSuccess)
                {
                    order.PaymentStatus = PaymentStatus.PaymentPaid.ToString();
                    order.PaymentAmount = request.PaymentResponse.PaymentAmount;
                    order.PaymentDate = DateTime.Now;
                }


                _entities.OrderService.Update(order);

                // SaveChange
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
