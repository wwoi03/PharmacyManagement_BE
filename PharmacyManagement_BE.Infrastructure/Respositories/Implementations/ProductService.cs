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
            var dateThreshold = DateTime.Now.AddDays(15);
            var random = new Random();

            // Lấy danh sách sản phẩm còn hạn trước 15 ngày
            var products = _context.ShipmentDetails
                .Where(sdItem => sdItem.ExpirationDate > dateThreshold)
                .Include(sdItem => sdItem.Product)
                .GroupBy(sdItem => new { sdItem.ProductId })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                    TotalSold = group.Sum(sale => sale.Sold) // tính tổng sản phẩm bán được
                })
                .Distinct()
                .Where(group => group.TotalSold > 0)
                .ToList();

            // Thông tin bổ sung
            var result = products.Select(itemProduct =>
            {
                // Lấy chi tiết đơn nhập mới nhất
                var shipmentDetails = _context.ShipmentDetails
                    .Where(sdItem => sdItem.ProductId == itemProduct.ProductId && sdItem.ExpirationDate > dateThreshold)
                    .Include(sdItem => sdItem.Shipment)
                    .Include(sdItem => sdItem.Product)
                    .OrderBy(sdItem => sdItem.Shipment.ImportDate)
                    .FirstOrDefault();

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
                    .Where(promItem => promItem.ProductId == itemProduct.ProductId)
                    .Include(promItem => promItem.Promotion)
                    .FirstOrDefault(promItem => promItem.Promotion.EndDate >= DateTime.Now);

                return new ItemProductDTO
                {
                    ProductId = itemProduct.ProductId,
                    ProductName = shipmentDetails.Product.Name,
                    Specifications = shipmentDetails.Product.Specifications,
                    ProductImage = shipmentDetails.Product.Image,
                    ShipmentDetailsId = shipmentDetails.Id,
                    Discount = promotion?.Promotion?.DiscountValue ?? 0,
                    ShipmentDetailsUnits = shipmentDetailsUnit,
                };
            })
            .Take(12) // Giới hạn số lượng sản phẩm
            .ToList();

            return result;
        }

        public async Task<List<ItemProductDTO>> GetNewProducts()
        {
            var dateThreshold = DateTime.Now.AddDays(15);

            // Lấy danh sách sản phẩm còn hạn trước 15 ngày
            var products = _context.ShipmentDetails
                .Where(sdItem => sdItem.ExpirationDate > dateThreshold)
                .Include(sdItem => sdItem.Product)
                .OrderByDescending(sdItem => sdItem.ExpirationDate)
                .GroupBy(sdItem => new { sdItem.ProductId })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                    Count = group.Count(),
                })
                .Distinct()
                .ToList();

            // Tìm sản phẩm có số lần nhập ít nhất
            var minEntryCount = products.Min(g => g.Count);

            // Lọc các sản phẩm có số lần nhập ít nhất
            var productsWithMinEntries = products
                .Where(g => g.Count == minEntryCount)
                .Select(g => new ItemProductDTO
                {
                    ProductId = g.ProductId,
                })
                .ToList();

            // Thông tin bổ sung
            var result = productsWithMinEntries.Select(itemProduct => 
            {
                // Lấy chi tiết đơn nhập mới nhất
                var shipmentDetails = _context.ShipmentDetails
                    .Where(sdItem => sdItem.ProductId == itemProduct.ProductId && sdItem.ExpirationDate > dateThreshold)
                    .Include(sdItem => sdItem.Shipment)
                    .Include(sdItem => sdItem.Product)
                    .OrderBy(sdItem => sdItem.Shipment.ImportDate)
                    .FirstOrDefault();

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
                    .Where(promItem => promItem.ProductId == itemProduct.ProductId)
                    .Include(promItem => promItem.Promotion)
                    .FirstOrDefault(promItem => promItem.Promotion.EndDate >= DateTime.Now);

                return new ItemProductDTO
                {
                    ProductId = itemProduct.ProductId,
                    ProductName = shipmentDetails.Product.Name,
                    Specifications = shipmentDetails.Product.Specifications,
                    ProductImage = shipmentDetails.Product.Image,
                    ShipmentDetailsId = shipmentDetails.Id,
                    Discount = promotion?.Promotion?.DiscountValue ?? 0,
                    ShipmentDetailsUnits = shipmentDetailsUnit,
                };
            })
            .Take(12) // Giới hạn số lượng sản phẩm
            .ToList();

            return result;
        }

        public async Task<List<ItemProductDTO>> GetSaleProducts()
        {
            var dateThreshold = DateTime.Now.AddDays(15);

            // Lấy danh sách sản phẩm còn hạn trước 15 ngày
            var products = _context.ShipmentDetails
                .Where(sdItem => sdItem.ExpirationDate > dateThreshold)
                .Include(sdItem => sdItem.Product)
                .GroupBy(sdItem => new { sdItem.ProductId })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                })
                .Distinct()
                .ToList();

            // Thông tin bổ sung
            var result = products.Select(itemProduct =>
            {
                // Lấy chi tiết đơn nhập mới nhất
                var shipmentDetails = _context.ShipmentDetails
                    .Where(sdItem => sdItem.ProductId == itemProduct.ProductId && sdItem.ExpirationDate > dateThreshold)
                    .Include(sdItem => sdItem.Shipment)
                    .Include(sdItem => sdItem.Product)
                    .OrderBy(sdItem => sdItem.Shipment.ImportDate)
                    .FirstOrDefault();

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
                    .Where(promItem => promItem.ProductId == itemProduct.ProductId)
                    .Include(promItem => promItem.Promotion)
                    .FirstOrDefault(promItem => promItem.Promotion.EndDate >= DateTime.Now);

                if (promotion == null)
                    return null;

                return new ItemProductDTO
                {
                    ProductId = itemProduct.ProductId,
                    ProductName = shipmentDetails.Product.Name,
                    Specifications = shipmentDetails.Product.Specifications,
                    ProductImage = shipmentDetails.Product.Image,
                    ShipmentDetailsId = shipmentDetails.Id,
                    Discount = promotion?.Promotion?.DiscountValue ?? 0,
                    ShipmentDetailsUnits = shipmentDetailsUnit,
                };
            })
            .Where(item => item != null)
            .Take(12)
            .ToList();

            return result;
        }
        #endregion EF & LinQ
    }
}