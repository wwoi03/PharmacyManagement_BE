using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsUnitFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsUnitFeatures.Handlers
{
    internal class GetShipmentDetailsUnitBestestQueryHandler : IRequestHandler<GetShipmentDetailsUnitBestestQueryRequest, ResponseAPI<List<ShipmentDetailsUnitDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetShipmentDetailsUnitBestestQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<ShipmentDetailsUnitDTO>>> Handle(GetShipmentDetailsUnitBestestQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.ShipmentDetailsUnitService.GetShipmentDetailsBestest(request.ProductId);

                return new ResponseSuccessAPI<List<ShipmentDetailsUnitDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ShipmentDetailsUnitDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
