using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
