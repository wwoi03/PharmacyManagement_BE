using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Handlers
{
    internal class GetCostStatisticsShipmentQueryHandler : IRequestHandler<GetCostStatisticsShipmentQueryRequest, ResponseAPI<List<CostStatisticsShipmentDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCostStatisticsShipmentQueryHandler(IPMEntities entities)
        {
            this._entities= entities;
        }

        public async Task<ResponseAPI<List<CostStatisticsShipmentDTO>>>Handle(GetCostStatisticsShipmentQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // kiểm tra chi nhánh tồn tại
                var branchExists = await _entities.BranchService.GetById(request.BranchId);

                if (branchExists == null)
                    return new ResponseErrorAPI<List<CostStatisticsShipmentDTO>>(StatusCodes.Status404NotFound, "Chi nhánh không tồn tại.");

                var response = new List<CostStatisticsShipmentDTO>();
                if (string.IsNullOrEmpty(request.SupplierName))
                    response = await _entities.ShipmentService.GetCostStatisticsShipment(request.BranchId, request.FromDate, request.ToDate);
                else
                    response = await _entities.ShipmentService.GetCostStatisticsShipmentByMonth(request.BranchId, request.FromDate, request.ToDate, request.SupplierName);

                return new ResponseSuccessAPI<List<CostStatisticsShipmentDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<CostStatisticsShipmentDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }

        private int CalculateNumberOfMonths(DateTime startDate, DateTime endDate)
        {
            int months = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;
            return months;
        }
    }
}
