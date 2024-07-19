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

        #region Queries Dapper
        public async Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipment(Guid branchId, DateTime fromDate, DateTime toDate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BranchId", branchId);
            parameters.Add("@FromDate", fromDate);
            parameters.Add("@ToDate", toDate);

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
                    SELECT s.Id, s.ImportDate, s.CodeShipment as CodeShipment, s.Note, s.Status, sp.Name as SupplierName, COUNT(DISTINCT sd.ProductId) as TotalProduct, SUM(sd.Quantity) as TotalQuantity
                    FROM Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE s.BranchId = @BranchId
                    GROUP BY s.Id, s.ImportDate, s.CodeShipment, s.Note, s.Status, sp.Name";

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
        #endregion

        #region Queries EntityFramework & LinQ
        public async Task<List<Shipment>> GetAllShipmentByStaffId(Guid id)
        {
            return _context.Shipments.Where(x => x.StaffId == id).ToList();
        }

        public async Task<List<ShipmentDetails>> GetShipmentDetailsByShipment(Guid shipmentId)
        {
            return _context.ShipmentDetails.Where(s => s.ShipmentId == shipmentId).ToList();
        }

        public async Task<DetailsShipmentDTO?> GetShipmentDetails(Guid shipmentId)
        {
            return _context.Shipments
                .Where(i => i.Id == shipmentId)
                .Select(i => new DetailsShipmentDTO
                {
                    ShipmentId = i.Id,
                    CodeShipment = i.CodeShipment,
                    ImportDate = i.ImportDate,
                    Note = i.Note,
                    Status = i.Status,
                    SupplierId = i.SupplierId,
                    BranchId = i.BranchId

                }).FirstOrDefault();
        }

        /*public async Task<List<ShipmentDTO>> SearchShipments(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName)
        {
            return _context.Shipments
                .Where(s => s.BranchId == branchId && s.ImportDate >= fromDate && s.ImportDate <= toDate)
                .Join(_context.Suppliers, // Thực hiện inner join với bảng Suppliers
                    s => s.SupplierId,
                    sp => sp.Id,
                    (s, sp) => new { Shipment = s, Supplier = sp })
                .Where(s => s.Supplier.Name.Contains(supplierName)) // Lọc theo tên nhà cung cấp
                .GroupJoin(_context.ShipmentDetails, // GroupJoin với ShipmentDetails
                    s => s.Shipment.Id,
                    sd => sd.ShipmentId,
                    (s, sd) => new { s.Shipment, s.Supplier, ShipmentDetails = sd })
                .SelectMany(
                    temp => temp.ShipmentDetails.DefaultIfEmpty(), // Sử dụng DefaultIfEmpty để xử lý trường hợp không có chi tiết nào
                    (temp, sd) => new { temp.Shipment, temp.Supplier, ShipmentDetail = sd })
                .GroupBy(
                    temp => new { temp.Shipment.Id, temp.Shipment.ImportDate, temp.Shipment.Note, temp.Shipment.Status, SupplierName = temp.Supplier.Name },
                    temp => temp.ShipmentDetail)
                .Select(g => new ShipmentDTO
                {
                    Id = g.Key.Id,
                    ImportDate = g.Key.ImportDate,
                    Note = g.Key.Note,
                    Status = g.Key.Status,
                    SupplierName = g.Key.SupplierName,
                    TotalProduct = g.Select(sd => sd.ProductId).Distinct().Count(), // Đếm số lượng sản phẩm khác nhau
                    TotalQuantity = g.Sum(sd => sd.Quantity)
                })
                .ToList();
        }*/

        /*public async Task<List<ShipmentDTO>> GetShipmentsByBranch(Guid branchId)
        {
            return _context.Shipments
                .Where(s => s.BranchId == branchId)
                .GroupJoin(_context.ShipmentDetails,
                    s => s.Id,
                    sd => sd.ShipmentId,
                    (s, sdGroup) => new { Shipment = s, ShipmentDetails = sdGroup })
                .SelectMany(
                    temp => temp.ShipmentDetails.DefaultIfEmpty(),
                    (temp, sd) => new { temp.Shipment, ShipmentDetail = sd })
                .GroupBy(
                    temp => new { temp.Shipment.Id, temp.Shipment.ImportDate, temp.Shipment.Note, temp.Shipment.Status, temp.Shipment.Supplier.Name },
                    temp => temp.ShipmentDetail)
                .Select(g => new ShipmentDTO
                {
                    Id = g.Key.Id,
                    ImportDate = g.Key.ImportDate,
                    Note = g.Key.Note,
                    Status = g.Key.Status,
                    SupplierName = g.Key.Name,
                    TotalProduct = g.Select(sd => sd.ProductId).Distinct().Count(),
                    TotalQuantity = g.Sum(sd => sd.Quantity)
                })
                .ToList();
        }*/

        /*public async Task<List<CostStatisticsShipmentDTO>> GetCostStatisticsShipmentByMonth(Guid branchId, DateTime fromDate, DateTime toDate, string supplierName)
        {
            return await _context.Shipments
                .Where(s => s.BranchId == branchId && s.ImportDate >= fromDate && s.ImportDate <= toDate)
                .Join(_context.Suppliers,
                    shipment => shipment.SupplierId,
                    supplier => supplier.Id,
                    (shipment, supplier) => new { shipment, supplier })
                .Where(s => s.supplier.Name.Contains(supplierName))
                .GroupJoin(_context.ShipmentDetails,
                    ss => ss.shipment.Id,
                    shipmentDetails => shipmentDetails.ShipmentId,
                    (ss, shipmentDetails) => new { ss.shipment, ss.supplier, shipmentDetails })
                .SelectMany(
                    ss => ss.shipmentDetails.DefaultIfEmpty(),
                    (ss, shipmentDetails) => new { ss.shipment, ss.supplier, shipmentDetails })
                .GroupBy(
                    temp => new { temp.supplier.Name, Year = temp.shipment.ImportDate.Year, Month = temp.shipment.ImportDate.Month },
                    temp => temp.shipmentDetails)
                .Select(g => new CostStatisticsShipmentDTO
                {
                    SupplierName = g.Key.Name,
                    TotalCost = g.Sum(shipmentDetails => shipmentDetails.Quantity * shipmentDetails.ImportPrice),
                    TotalProduct = g.Select(shipmentDetails => shipmentDetails != null ? shipmentDetails.ProductId : 0).Distinct().Count(),
                    TotalQuantity = g.Sum(shipmentDetails => shipmentDetails != null ? shipmentDetails.Quantity : 0),
                    FromDate = new DateTime(g.Key.Year, g.Key.Month, 1),
                    ToDate = new DateTime(g.Key.Year, g.Key.Month, DateTime.DaysInMonth(g.Key.Year, g.Key.Month))
                })
                .OrderBy(summary => summary.FromDate)
                .ToList();
        }*/
        #endregion
    }
}
