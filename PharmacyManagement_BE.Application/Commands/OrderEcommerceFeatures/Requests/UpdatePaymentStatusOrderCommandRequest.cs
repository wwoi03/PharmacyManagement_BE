using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests
{
    public class UpdatePaymentStatusOrderCommandRequest : IRequest<ResponseAPI<string>>
    {
        public PaymentResponseDTO PaymentResponse { get; set; } 
    }
}
