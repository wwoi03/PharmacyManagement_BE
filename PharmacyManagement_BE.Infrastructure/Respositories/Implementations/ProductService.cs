using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs;
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
    public class ProductService : RepositoryService<Product>, IProductService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public ProductService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        #region EF & LinQ
        public async Task<List<ListProductDTO>> SearchProducts(string ContentStr, string CategoryName)
        {
            return _context.Products
                .Include(i => i.Category)
                .Select(i => new ListProductDTO
                {
                    Id = i.Id,
                    CodeMedicine = i.CodeMedicine,
                    ProductName = i.Name,
                    Image = i.Image,
                    CategoryName = i.Category.Name,
                    BrandOrigin = i.BrandOrigin,
                    ShortDescription = i.ShortDescription,
                })
                .ToList();
        }

        public async Task<List<ListProductDTO>> GetProducts()
        {
            return _context.Products
                .Include(i => i.Category)
                .Select(i => new ListProductDTO
                {
                    Id = i.Id,
                    CodeMedicine = i.CodeMedicine,
                    ProductName = i.Name,
                    Image = i.Image,
                    CategoryName = i.Category.Name,
                    BrandOrigin = i.BrandOrigin,
                    ShortDescription = i.ShortDescription,
                })
                .ToList();
        }

        public async Task<Product?> GetProductByCodeMedicineOrName(string codeMedicine, string name)
        {
            return _context.Products.FirstOrDefault(i => i.CodeMedicine.Equals(codeMedicine) || i.Name.Equals(name));
        }

        /*public async Task<List<ItemProductDTO>> GetSellingProductByMonthYear(int month, int year)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Month", month);
            parameters.Add("@Year", year);

            var sql = @"
                    SELECT 
                        p.Id AS ProductId,
                        sd.Id AS ShipmentDetailsId,
                        p.Name AS ProductName,
                        p.Specifications AS Specifications,
                        p.Image AS ProductImage,
                        pr.DiscountValue AS Discount
                    FROM 
	                    Products p
	                    LEFT JOIN ShipmentDetails sd ON p.Id = sd.ProductId
	                    LEFT JOIN PromotionProducts pp ON p.Id = pp.ProductId
	                    LEFT JOIN Promotions pr ON pp.PromotionId = pr.Id
                    WHERE 
                        sd.ManufactureDate = (
                            SELECT MAX(sd2.ManufactureDate)
                            FROM ShipmentDetails sd2
                            WHERE sd2.ProductId = p.Id
                        )";

            var result = (await _dapperContext.GetConnection.QueryAsync<ItemProductDTO>(sql, parameters)).ToList();

            foreach (var item in result)
            {
                item.ShipmentDetailsUnits =
                    _context
                        .ShipmentDetailsUnit
                        .Where(i => i.ShipmentDetailsId == item.ShipmentDetailsId)
                        .Join(_context.Units,
                            sduItem => sduItem.UnitId,
                            u => u.Id,
                            (sduItem, u) => new { shipmentDetailsUnit = sduItem, unit = u })
                        .Select(temp => new ShipmentDetailsUnitDTO
                        {
                            Id = temp.unit.Id,
                            Name = temp.unit.Name,
                            SalePrice = temp.shipmentDetailsUnit.SalePrice

                        })
                        .ToList();
            }

            return result;
        }*/

        public async Task<List<SelectProductDTO>> GetProductsSelect()
        {
            return _context.Products
                .Select(p => new SelectProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CodeMedicine = p.CodeMedicine,
                })
                .ToList();
        }

        public async Task<List<ItemProductDTO>> GetSellingProductByMonthYear(int month, int year)
        {
            var products = _context.Products
                .Select(item => new ItemProductDTO
                {
                    ProductId = item.Id,
                    ProductName = item.Name,
                    Specifications = item.Specifications,
                    ProductImage = item.Image,
                })
                .ToList();

            var result = products.Select(item =>
            {
                // Lấy chi tiết đơn nhập
                var shipmentDetails = _context.ShipmentDetails
                    .Where(sdItem => sdItem.ProductId == item.ProductId
                        /*&& sdItem.ExpirationDate > DateTime.Now*/)
                    .OrderByDescending(sdItem => sdItem.ExpirationDate)
                    .FirstOrDefault();

                if (shipmentDetails == null)
                    return null;

                // Lấy đơn giá
                var shipmentDetailsUnit = _context.ShipmentDetailsUnit
                    .Where(sduItem => sduItem.ShipmentDetailsId == shipmentDetails.Id)
                    .Select(sduItem => new ShipmentDetailsUnitDTO
                    {
                        UnitId = sduItem.Unit.Id,
                        CodeUnit = sduItem.Unit.Name,
                        UnitName = sduItem.Unit.NameDetails,
                        SalePrice = sduItem.SalePrice,
                        UnitCount = sduItem.UnitCount,
                        Level = sduItem.Level
                    })
                    .OrderBy(sduItem => sduItem.Level)
                    .ToList();

                // Lấy giảm giá 
                var promotion = _context.PromotionProducts
                    .Where(promItem => promItem.ProductId == item.ProductId)
                    .Include(promItem => promItem.Promotion)
                    .FirstOrDefault(promItem => promItem.Promotion.EndDate > DateTime.Now);

                return new ItemProductDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Specifications = item.Specifications,
                    ProductImage = item.ProductImage,
                    ShipmentDetailsId = shipmentDetails.Id,
                    Discount = promotion?.Promotion?.DiscountValue ?? 0,
                    ShipmentDetailsUnits = shipmentDetailsUnit,
                };
            })
            .Where(item => item != null)
            .ToList();

            return result;
        }
        #endregion EF & LinQ
    }
}