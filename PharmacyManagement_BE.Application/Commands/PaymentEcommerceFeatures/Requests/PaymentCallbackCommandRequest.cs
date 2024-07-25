using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PaymentEcommerceFeatures.Requests
{
    public class PaymentCallbackCommandRequest : IRequest<ResponseAPI<PaymentResponseDTO>>
    {
        public IQueryCollection Collections { get; set; }
    }
}
