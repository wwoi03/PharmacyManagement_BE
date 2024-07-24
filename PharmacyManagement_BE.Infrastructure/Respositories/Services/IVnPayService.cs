using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymenDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymentEcommerceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformationDTO model, HttpContext context);
        Task<PaymentResponseDTO> PaymentExecute(IQueryCollection collections);
    }
}
