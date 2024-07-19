using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsUnitFeatures.Requests
{
    public class GetShipmentDetailsUnitBestestQueryRequest : IRequest<ResponseAPI<List<ShipmentDetailsUnitDTO>>>
    {
        public Guid ProductId { get; set; }
    }
}
