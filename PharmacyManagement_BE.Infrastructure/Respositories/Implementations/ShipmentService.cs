﻿using Dapper;
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

        public async Task<List<ShipmentDTO>> GetShipmentsByBranch(Guid branchId)
        {
            var sql = @"
                    SELECT s.Id, s.ImportDate, s.Note, s.Status, sp.Name as SupplierName, COUNT(DISTINCT sd.ProductId) as TotalProduct, SUM(sd.Quantity) as TotalQuantity
                    FROM Shipments s 
	                    INNER JOIN Suppliers sp ON s.SupplierId = sp.Id
	                    LEFT JOIN ShipmentDetails sd ON s.Id = sd.ShipmentId
                    WHERE s.BranchId = @BranchId
                    GROUP BY s.Id, s.ImportDate, s.Note, s.Status, sp.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ShipmentDTO>(sql, new { BranchId = branchId.ToString() })).ToList();
        }
    }
}
