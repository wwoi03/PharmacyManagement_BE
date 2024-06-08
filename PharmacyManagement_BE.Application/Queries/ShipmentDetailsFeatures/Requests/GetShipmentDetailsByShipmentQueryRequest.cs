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
    public class GetShipmentDetailsByShipmentQueryRequest : IRequest<ResponseAPI<List<ListShipmentDetailsDTOs>>>
    {
        public Guid ShipmentId { get; set; }
    }
}
