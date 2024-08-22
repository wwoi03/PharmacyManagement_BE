using Dapper;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
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
    public class ShipmentDetailsService : RepositoryService<ShipmentDetails>, IShipmentDetailsService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public ShipmentDetailsService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        #region Dapper
        public async Task<DetailsShipmentDetailsDTO> GetDetailsShipmentDetails(Guid shipmentDetailsId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ShipmentDetailsId", shipmentDetailsId);

            var sql = @"
                    SELECT 
                        sd.Id AS ShipmentDetailsId,
                        sl.Name AS SupplierName,
                        p.Id AS ProductId,
                        p.Name AS ProductName,
                        p.Image AS ProductImage,
                        sd.ImportPrice AS ImportPrice,
                        sd.Quantity AS Quantity,
                        sd.Sold AS Sold,
                        sd.ManufactureDate AS ManufactureDate,
                        sd.ExpirationDate AS ExpirationDate,
                        sd.Note AS Note,
                        sd.ProductionBatch AS ProductionBatch
                    FROM ShipmentDetails sd 
                        INNER JOIN Shipments s ON sd.ShipmentId = s.Id
                        INNER JOIN Suppliers sl ON s.SupplierId = sl.Id
                        INNER JOIN Products p ON sd.ProductId = p.Id
                    WHERE sd.Id = @ShipmentDetailsId
                    ORDER BY sd.ImportPrice";

            return _dapperContext.GetConnection.QueryFirst<DetailsShipmentDetailsDTO>(sql, parameters);
        }

        public async Task<List<ListShipmentDetailsDTO>> GetShipmentDetailsByShipment(Guid shipmentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ShipmentId", shipmentId);

            var sql = @"
                    SELECT 
                        sd.Id AS ShipmentDetailsId,
                        p.Name AS ProductName,
                        p.Image AS ProductImage,
                        sd.ImportPrice AS ImportPrice,
                        sd.Quantity AS Quantity,
                        sd.Sold AS Sold,
                        sd.ManufactureDate AS ManufactureDate,
                        sd.ExpirationDate AS ExpirationDate
                    FROM ShipmentDetails sd INNER JOIN Products p ON sd.ProductId = p.Id
                    WHERE sd.ShipmentId = @ShipmentId
                    ORDER BY sd.ImportPrice";

            return (await _dapperContext.GetConnection.QueryAsync<ListShipmentDetailsDTO>(sql, parameters)).ToList();
        }


        #endregion Dapper

        #region EntityFramework & LinQ
        public async Task<bool> CreateRangeShipmentDetails(List<ShipmentDetails> shipmentDetails)
        {
            try
            {
                _context.ShipmentDetails.AddRange(shipmentDetails);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> RemoveRangeShipmentDetails(List<ShipmentDetails> shipmentDetails)
        {
            try
            {
                _context.ShipmentDetails.RemoveRange(shipmentDetails);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<List<ListShipmentDetailsDTO>> SearchShipmentDetailsByProduct(Guid shipmentId, string NameOrCodeMedicine)
        {
            return _context.ShipmentDetails
                .Where(sd => sd.ShipmentId == shipmentId)
                .Join(_context.Products,
                    shipmentDetails => shipmentDetails.ProductId,
                    product => product.Id,
                    (shipmentDetails, product) => new { shipmentDetails = shipmentDetails, product = product })
                .Where(temp => (temp.product.Name.Contains(NameOrCodeMedicine) || temp.product.CodeMedicine.Equals(NameOrCodeMedicine)))
                .Select(temp => new ListShipmentDetailsDTO
                {
                    ShipmentDetailsId = temp.shipmentDetails.Id,
                    ProductName = temp.product.Name,
                    ProductImage = temp.product.Image,
                    ManufactureDate = temp.shipmentDetails.ManufactureDate,
                    ExpirationDate = temp.shipmentDetails.ExpirationDate,
                    ImportPrice = temp.shipmentDetails.ImportPrice,
                    Quantity = temp.shipmentDetails.Quantity,
                    Sold = temp.shipmentDetails.Sold
                })
                .ToList();
        }

        public async Task<ShipmentDetails?> GetShipmentDetailsByProductId(Guid productId)
        {
            return _context.ShipmentDetails
                .FirstOrDefault(item => item.ProductId == productId);
        }

        #endregion EntityFramework & LinQ
    }
}
