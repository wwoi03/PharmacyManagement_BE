using Dapper;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductIngredientDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using System.Data;
using System.Data.Common;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs;

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

        public async Task<ProductDetailsEcommerceDTO>  GetProductWithDetails(Guid productId)
        {
            try
            {
                List<ShipmentDetailsUnitDTO_Hao> unit = new List<ShipmentDetailsUnitDTO_Hao>();
                //Lấy shipmentdetailsId
                ShipmentDetails shipmentDetails = await _context.ShipmentDetails.FirstOrDefaultAsync(pi => pi.ProductId == productId);
                
                if(shipmentDetails != null)
                {
                    var parameters = new DynamicParameters();

                    string sql = @"SELECT 
                    sd.ShipmentDetailsId,
                    sd.UnitId,
                    u.Name ,
                    sd.SalePrice
                FROM 
                    ShipmentDetailsUnit sd
                JOIN 
                Units u ON sd.UnitId = u.Id
                WHERE sd.ShipmentDetailsId = @ShipmentDetailsId";


                    parameters.Add("@ShipmentDetailsId", shipmentDetails.Id, DbType.Guid);

                    unit = (await _dapperContext.GetConnection.QueryAsync<ShipmentDetailsUnitDTO_Hao>(sql, parameters)).AsList();
                }
                

                return _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductDetailsEcommerceDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CodeMedicine = p.CodeMedicine,
                    Specifications = p.Specifications,
                    ShortDescription = p.ShortDescription,
                    Description = p.Description,
                    Uses = p.Uses,
                    HowToUse = p.HowToUse,
                    SideEffects = p.SideEffects,
                    Warning = p.Warning,
                    Preserve = p.Preserve,
                    Dosage = p.Dosage,
                    Contraindication = p.Contraindication,
                    DosageForms = p.DosageForms,
                    RegistrationNumber = p.RegistrationNumber,
                    BrandOrigin = p.BrandOrigin,
                    AgeOfUse = p.AgeOfUse,
                    View = p.View,
                    CartView = p.CartView,
                    CategoryId = p.CategoryId,
                    Image = p.Image,
                    ProductIngredients = _context.ProductIngredients.Where(pi => pi.ProductId == productId)
                        .Include(pi => pi.Ingredient)
                        .Include(pi => pi.Unit)
                        .Select(pi => new DetailsProductIngredientDTO
                        {
                            ProductId = pi.ProductId,
                            IngredientId = pi.IngredientId,
                            IngredientName = pi.Ingredient.Name,
                            CodeIngredient = pi.Ingredient.CodeIngredient,
                            Content = pi.Content,
                            UnitId = pi.UnitId,
                            UnitName = pi.Unit.Name,
                        })
                        .ToList(),
                    ProductSupports = _context.ProductSupports.Where(ps => ps.ProductId == p.Id).Select(ps => ps.SupportId).ToList(),
                    ProductDiseases = _context.ProductDiseases.Where(pd => pd.ProductId == p.Id).Select(pd => pd.DiseaseId).ToList(),
                    ShipmentDetailsUnit = unit,
            })
                .FirstOrDefault();
            }catch(Exception ex)
            {
                return new ProductDetailsEcommerceDTO();
            }
            
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
                            sdu => sdu.UnitId,
                            u => u.Id,
                            (sdu, u) => new { shipmentDetailsUnit = sdu, unit = u })
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
        #endregion EF & LinQ

        #region Dapper
        public Task<List<ItemProductDTO>> GetSellingProductByMonthYear(int month, int year)
        {
            throw new NotImplementedException();
        }

        //lấy danh sách top 10 sản phẩm được ưa chuộn tính = View
        public async Task<List<StatisticProductDTO>> GetTopView()
        {
            List<StatisticProductDTO> topView = new List<StatisticProductDTO>();

            //truy vấn top 10
            string sql = @"SELECT TOP 10 Id, Name, [View], CartView, Image
                        FROM Products
                        ORDER BY [View] DESC";

            topView = (await _dapperContext.GetConnection.QueryAsync<StatisticProductDTO>(sql)).ToList();


            return topView;
        }

        public async Task<List<StatisticProductOrderDTO>> GetTopCanceledProduct()
        {
            List<StatisticProductOrderDTO> cancel = new List<StatisticProductOrderDTO>();

            //Truy vấn top 10 đơn bị hủy
            string sql = @"SELECT TOP 10 
                    P.Id, 
                    P.Name,  
                    P.CartView, 
                    P.Image, 
                    COUNT(O.Status) AS NumCanceled
                FROM 
                    Products AS P
                    INNER JOIN ShipmentDetails AS S ON P.Id = S.ProductId
                    INNER JOIN OrderDetails AS OD ON S.Id = OD.ShipmentDetailsId
                    INNER JOIN Orders AS O ON OD.OrderId = O.Id
                WHERE 
                    O.Status = 'CancellationOrderApproved'
                GROUP BY 
                    P.Id, 
                    P.Name, 
                    P.[View], 
                    P.CartView, 
                    P.Image
                ORDER BY 
                    NumCanceled DESC";

            cancel = (await _dapperContext.GetConnection.QueryAsync<StatisticProductOrderDTO>(sql)).ToList();

            return cancel;
        }

        public async Task<List<StatisticProductOrderDTO>> GetTopSoldProduct()
        {
            List<StatisticProductOrderDTO> sold = new List<StatisticProductOrderDTO>();

            //Truy vấn top 10 đơn bị hủy
            string sql = @"SELECT TOP 10 
                    P.Id, 
                    P.Name,  
                    P.CartView, 
                    P.Image, 
                    COUNT(O.Status) AS NumCanceled
                FROM 
                    Products AS P
                    INNER JOIN ShipmentDetails AS S ON P.Id = S.ProductId
                    INNER JOIN OrderDetails AS OD ON S.Id = OD.ShipmentDetailsId
                    INNER JOIN Orders AS O ON OD.OrderId = O.Id
                WHERE 
                    O.Status = 'OrderDelivered'
                GROUP BY 
                    P.Id, 
                    P.Name, 
                    P.[View], 
                    P.CartView, 
                    P.Image
                ORDER BY 
                    NumCanceled DESC";

            sold = (await _dapperContext.GetConnection.QueryAsync<StatisticProductOrderDTO>(sql)).ToList();

            return sold;
        }


        //Filter
        public async Task<List<FilterProductDTO>> GetFilterProducts(int PageNumber, int RowsPerPage, Guid? selectCategory, PriceType? Price, List<Guid> Support, List<Guid> Disease)
        {
            var parameters = new DynamicParameters();
            List<FilterProductDTO> listFilter = new List<FilterProductDTO>();

            string sql = @"SELECT DISTINCT P.Id, P.Name, P.Image
                       FROM Products P
                       LEFT JOIN ProductSupports PS ON P.Id = PS.ProductId
                       LEFT JOIN ProductDiseases PD ON P.Id = PD.ProductId
                       LEFT JOIN ShipmentDetails SD ON P.Id = SD.ProductId
                       LEFT JOIN ShipmentDetailsUnit SDU ON SD.Id = SDU.ShipmentDetailsId
                       WHERE 1=1"; // Điều kiện luôn đúng để dễ dàng thêm các điều kiện khác

            // Điều kiện lọc theo Category
            if (selectCategory.HasValue)
            {
                sql += " AND CategoryId = @CategoryId";
                parameters.Add("@CategoryId", selectCategory.Value, DbType.Guid);
            }

            // Điều kiện lọc theo Price
            if (Price.HasValue)
            {
                decimal minPrice = 0, maxPrice = 100000;

                switch (Price.Value)
                {
                    case PriceType.Under100:
                        minPrice = 0;
                        maxPrice = 100000;
                        break;
                    case PriceType.From100To300:
                        minPrice = 100000;
                        maxPrice = 300000;
                        break;
                    case PriceType.From300To500:
                        minPrice = 300000;
                        maxPrice = 500000; 
                        break;
                    case PriceType.MoreThan500:
                        minPrice = 500000;
                        maxPrice = 1e9m;
                        break;
                }

                sql += " AND Price BETWEEN @MinPrice AND @MaxPrice";
                parameters.Add("@MinPrice", minPrice, DbType.Decimal);
                parameters.Add("@MaxPrice", maxPrice, DbType.Decimal);
            }

            // Điều kiện lọc theo Support
            if (Support != null && Support.Count > 0)
            {
                sql += " AND PS.SupportId IN @SupportIds";
                parameters.Add("@SupportIds", Support, DbType.Guid);
            }

            // Điều kiện lọc theo Disease
            if (Disease != null && Disease.Count > 0)
            {
                sql += " AND PD.DiseaseId IN @DiseaseIds";
                parameters.Add("@DiseaseIds", Disease, DbType.Guid);
            }

            // Thêm phần sắp xếp và phân trang
            sql += @" ORDER BY [View] DESC
                  OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
                  FETCH NEXT @RowsPerPage ROWS ONLY";

            parameters.Add("@PageNumber", PageNumber, DbType.Int32);
            parameters.Add("@RowsPerPage", RowsPerPage, DbType.Int32);

            // Thực hiện truy vấn và trả về kết quả
            listFilter = (await _dapperContext.GetConnection.QueryAsync<FilterProductDTO>(sql, parameters)).AsList();
            return listFilter;
        }

        #endregion Dapper
    }
}