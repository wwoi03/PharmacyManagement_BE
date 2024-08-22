using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupportFeatures.Handlers
{
    internal class GetSupportSelectQueryHandler : IRequestHandler<GetSupportSelectQueryRequest, ResponseAPI<List<SelectSupportDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetSupportSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectSupportDTO>>> Handle(GetSupportSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.SupportService.GetSupportSelect();

                return new ResponseSuccessAPI<List<SelectSupportDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectSupportDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
