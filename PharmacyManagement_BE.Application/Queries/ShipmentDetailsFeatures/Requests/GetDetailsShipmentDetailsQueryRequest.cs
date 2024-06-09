using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests
{
    public class GetDetailsShipmentDetailsQueryRequest : IRequest<ResponseAPI<DetailsShipmentDetailsDTO>>
    {
        public Guid ShipmentDetailsId { get; set; }
        
    }
}
