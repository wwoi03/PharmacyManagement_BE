using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymenDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PaymentFeatures.Requests
{
    public class CreatePaymentUrlCommandRequest : IRequest<ResponseAPI<string>>
    {
        public PaymentInformationModel Model { get; set; }
        public HttpContext Context { get; set; }
    }
}
