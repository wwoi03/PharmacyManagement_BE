using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Handlers
{
    internal class GetDetailsShipmentDetailsQueryHandler : IRequestHandler<GetDetailsShipmentDetailsQueryRequest, ResponseAPI<DetailsShipmentDetailsDTO>>
    {
        private readonly IPMEntities _entities;

        public GetDetailsShipmentDetailsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsShipmentDetailsDTO>> Handle(GetDetailsShipmentDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            
            try
            {
                // Lấy danh sách chi tiết đơn hàng
                var response = await _entities.ShipmentDetailsService.GetDetailsShipmentDetails(request.ShipmentDetailsId);

                return new ResponseSuccessAPI<DetailsShipmentDetailsDTO>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsShipmentDetailsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
