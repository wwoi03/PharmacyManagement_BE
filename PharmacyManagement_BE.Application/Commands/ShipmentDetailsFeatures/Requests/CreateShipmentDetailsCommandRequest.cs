using MediatR;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests
{
    public class CreateShipmentDetailsCommandRequest : IRequest<ResponseAPI<ShipmentDetailsRequest>>
    {
        public Guid ShipmentId { get; set; } 
        public List<ShipmentDetailsRequest> ShipmentDetails { get; set; }
    }
}
