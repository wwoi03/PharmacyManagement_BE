using Dapper;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs;
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
    public class ShipmentDetailsUnitService : RepositoryService<ShipmentDetailsUnit>, IShipmentDetailsUnitService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public ShipmentDetailsUnitService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        public async Task<List<ShipmentDetailsUnitDTO>> GetShipmentDetailsBestest(Guid productId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", productId);

            var sql = @"
                    SELECT 
                        u.Id AS UnitId,
                        u.Name AS CodeUnit,
                        u.NameDetails AS UnitName,
                        sdu.SalePrice AS SalePrice,
                        sdu.UnitCount AS UnitCount,
                        sdu.Level AS Level
                    FROM 
	                    ShipmentDetailsUnit sdu
	                    LEFT JOIN Units u ON u.Id = sdu.UnitId
                    WHERE 
	                    sdu.ShipmentDetailsId = 
		                    (
			                    SELECT TOP 1 sd.Id
			                    FROM ShipmentDetails sd
			                    WHERE 
				                    sd.ProductId = @ProductId
			                    ORDER BY sd.ImportPrice DESC
		                    )
                    ORDER BY sdu.Level";

            return (await _dapperContext.GetConnection.QueryAsync<ShipmentDetailsUnitDTO>(sql, parameters)).ToList();
        }
    }
}
