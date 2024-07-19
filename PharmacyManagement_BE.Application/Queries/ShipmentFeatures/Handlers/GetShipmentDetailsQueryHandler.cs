using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Handlers
{
    internal class GetShipmentDetailsQueryHandler : IRequestHandler<GetShipmentDetailsQueryRequest, ResponseAPI<DetailsShipmentDTO>>
    {
        private readonly IPMEntities _entities;

        public GetShipmentDetailsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsShipmentDTO>> Handle(GetShipmentDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.ShipmentService.GetShipmentDetails(request.ShipmentId);

                return new ResponseSuccessAPI<DetailsShipmentDTO>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsShipmentDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
