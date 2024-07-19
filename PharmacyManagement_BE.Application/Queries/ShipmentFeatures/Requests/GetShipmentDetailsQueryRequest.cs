using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests
{
    public class GetShipmentDetailsQueryRequest :IRequest<ResponseAPI<DetailsShipmentDTO>>
    {
        public Guid ShipmentId { get; set; }
    }
}
