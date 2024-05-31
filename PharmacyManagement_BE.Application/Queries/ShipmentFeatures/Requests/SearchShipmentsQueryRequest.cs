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
    public class SearchShipmentsQueryRequest : IRequest<ResponseAPI<List<ShipmentResponse>>>
    {
        public Guid BranchId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SupplierName { get; set; }
    }
}
