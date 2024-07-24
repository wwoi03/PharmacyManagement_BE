using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.PaymentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.PaymentFeatures.Handlers
{
    internal class CreatePaymentUrlCommandHandler : IRequestHandler<CreatePaymentUrlCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public CreatePaymentUrlCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<string>> Handle(CreatePaymentUrlCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //var response = await _entities.VnPayService.CreatePaymentUrl(request.Model, request.Context);

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }
        }
    }
}
