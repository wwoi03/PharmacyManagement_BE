using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.PaymentEcommerceFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PaymentEcommerceFeatures.Handlers
{
    internal class PaymentCallbackCommandHandler : IRequestHandler<PaymentCallbackCommandRequest, ResponseAPI<PaymentResponseDTO>>
    {
        private readonly IPMEntities _entities;

        public PaymentCallbackCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<PaymentResponseDTO>> Handle(PaymentCallbackCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.VnPayService.PaymentExecute(request.Collections);
                return new ResponseSuccessAPI<PaymentResponseDTO>(StatusCodes.Status200OK, response); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<PaymentResponseDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
