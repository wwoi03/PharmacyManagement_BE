using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IShipmentService : IRepositoryService<Shipment>
    {
        Task<List<Shipment>> GetAllShipmentByStaffId(Guid id);
        Task<List<ShipmentDTO>> GetShipmentsByBranch(Guid branchId);
        Task<List<ShipmentDTO>> SearchShipments(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName);
        Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipment(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName);
        Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipmentByMonth(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName);
    }
}
