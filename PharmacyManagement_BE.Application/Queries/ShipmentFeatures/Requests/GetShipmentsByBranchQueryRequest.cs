using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests
{
    public class GetShipmentsByBranchQueryRequest : IRequest<ResponseAPI<List<ShipmentResponse>>>
    {
        public Guid BranchId { get; set; }
    }
}
