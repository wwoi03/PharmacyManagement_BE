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
    public class GetCostStatisticsShipmentQueryRequest : IRequest<ResponseAPI<List<CostStatisticsShipmentDTO>>>
    {
        public Guid BranchId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SupplierName { get; set; }
    }
}
