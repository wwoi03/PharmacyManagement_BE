using Dapper;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class ShipmentService : RepositoryService<Shipment>, IShipmentService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public ShipmentService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        public async Task<List<Shipment>> GetAllShipmentByStaffId(Guid id)
        {
            return _context.Shipments.Where(x => x.StaffId == id).ToList();
        }

        public async Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipment(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BranchId", branchId);
            parameters.Add("@FromDate", fromDate);
            parameters.Add("@ToDate", toDate);
            parameters.Add("@SupplierName", $"%{supplierName}%");

            var sql = @"
                    SELECT sp.Name as SupplierName, 
                        SUM(sd.Quantity * sd.ImportPrice) as TotalCost, 
                        COUNT(sd.ProductId) as TotalProduct, 
                        SUM(sd.Quantity) as TotalQuantity, 
                        @FromDate as FromDate, 
                        @ToDate as ToDate
                    FROM Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE s.BranchId = @BranchId
                        AND s.ImportDate BETWEEN @FromDate AND @ToDate 
                        AND sp.Name LIKE @SupplierName
                    GROUP BY sp.Name";

            return (await _dapperContext.GetConnection.QueryAsync<CostStatisticsShipmentDTO>(sql, parameters)).ToList();
        }

        public async Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipmentByMonth(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BranchId", branchId);
            parameters.Add("@FromDate", fromDate);
            parameters.Add("@ToDate", toDate);
            parameters.Add("@SupplierName", $"%{supplierName}%");

            var sql = @"
                    SELECT 
                        sp.Name as SupplierName, 
                        SUM(sd.Quantity * sd.ImportPrice) as TotalCost, 
                        COUNT(sd.ProductId) as TotalProduct, 
                        SUM(sd.Quantity) as TotalQuantity, 
	                    DATEFROMPARTS(YEAR(s.ImportDate), MONTH(s.ImportDate), 1) as FromDate, 
                        EOMONTH(DATEFROMPARTS(YEAR(s.ImportDate), MONTH(s.ImportDate), 1)) as ToDate
                    FROM 
                        Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE 
                        s.BranchId = @BranchId
                        AND s.ImportDate BETWEEN @FromDate AND @ToDate 
                        AND sp.Name LIKE @SupplierName
                    GROUP BY sp.Name, MONTH(s.ImportDate), YEAR(s.ImportDate)
                    ORDER BY YEAR(s.ImportDate), MONTH(s.ImportDate)";

            return (await _dapperContext.GetConnection.QueryAsync<CostStatisticsShipmentDTO>(sql, parameters)).ToList();
        }

        public async Task<List<ShipmentDTO>> GetShipmentsByBranch(Guid branchId)
        {
            var sql = @"
                    SELECT s.Id, s.ImportDate, s.Note, s.Status, sp.Name as SupplierName, COUNT(DISTINCT sd.ProductId) as TotalProduct, SUM(sd.Quantity) as TotalQuantity
                    FROM Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE s.BranchId = @BranchId
                    GROUP BY s.Id, s.ImportDate, s.Note, s.Status, sp.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ShipmentDTO>(sql, new { BranchId = branchId })).ToList();
        }
       
        public async Task<List<ShipmentDTO>> SearchShipments(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BranchId", branchId);
            parameters.Add("@FromDate", fromDate);
            parameters.Add("@ToDate", toDate);
            parameters.Add("@SupplierName", $"%{supplierName}%");

            var sql = @"
                    SELECT s.Id, s.ImportDate, s.Note, s.Status, sp.Name as SupplierName, COUNT(DISTINCT sd.ProductId) as TotalProduct, SUM(sd.Quantity) as TotalQuantity
                    FROM Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE s.BranchId = @BranchId 
                        AND s.ImportDate BETWEEN @FromDate AND @ToDate 
                        AND sp.Name LIKE @SupplierName
                    GROUP BY s.Id, s.ImportDate, s.Note, s.Status, sp.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ShipmentDTO>(sql, parameters)).ToList();
        }
    }
}
