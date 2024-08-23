using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using RecommendSimilarProduct;
using RecommendSimilarProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class PredictionService : IPredictionService
    {
        private readonly PharmacyManagementContext _context;
        private readonly PMDapperContext _dapperContext;

        public PredictionService(PharmacyManagementContext context, PMDapperContext dapperContext) 
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        private async Task<List<ItemProductDTO>> GetProductByProductIds(List<Guid> productIds)
        {
            var dateThreshold = DateTime.Now.AddDays(15);

            var result = productIds.Select(itemProductId =>
            {
                // Lấy chi tiết đơn nhập mới nhất
                var shipmentDetails = _context.ShipmentDetails
                    .Where(sdItem => sdItem.ProductId == itemProductId && sdItem.ExpirationDate > dateThreshold)
                    .Include(sdItem => sdItem.Shipment)
                    .Include(sdItem => sdItem.Product)
                    .OrderBy(sdItem => sdItem.Shipment.ImportDate)
                    .FirstOrDefault();

                // Lấy đơn giá
                var shipmentDetailsUnit = _context.ShipmentDetailsUnit
                    .Where(sduItem => sduItem.ShipmentDetailsId == shipmentDetails.Id)
                    .Select(sduItem => new ShipmentDetailsUnitEDTO
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
                    .Where(promItem => promItem.ProductId == itemProductId)
                    .Include(promItem => promItem.Promotion)
                    .FirstOrDefault(promItem => promItem.Promotion.EndDate >= DateTime.Now);

                return new ItemProductDTO
                {
                    ProductId = itemProductId,
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

        private static long GetFiveDigitNumberFromGuid(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            long longFromGuid = BitConverter.ToInt64(bytes, 0);

            // Sử dụng modulo để thu được một số trong phạm vi 0 đến 99999
            long fiveDigitNumber = Math.Abs(longFromGuid) % 100000;

            return fiveDigitNumber;
        }

        public async Task<List<ProductInteractive>> GetSimilarProductInteractive()
        {
            List<ProductInteractive> productInteractives = new List<ProductInteractive>();

            // Lấy danh sách sản phẩm trong giỏ hàng
            var carts = _context.Carts
                .Select(item => new ProductInteractive
                {
                    CustomerId = GetFiveDigitNumberFromGuid(item.CustomerId),
                    ProductId = GetFiveDigitNumberFromGuid(item.ProductId),
                    Label = 0.5f,
                })
                .ToList();

            // Lấy danh sách sản phẩm được mua
            var orderDetails = _context.OrderDetails
                .Include(item => item.ShipmentDetails)
                .Select(item => new ProductInteractive
                {
                    CustomerId = GetFiveDigitNumberFromGuid(_context.Orders.FirstOrDefault(orderItem => orderItem.Id == item.OrderId).CustomerId),
                    ProductId = GetFiveDigitNumberFromGuid(item.ShipmentDetails.ProductId),
                    Label = 1f,
                })
                .ToList();

            productInteractives.AddRange(carts);
            productInteractives.AddRange(orderDetails);

            return productInteractives;
        }

        public async Task<List<ItemProductDTO>> GetSimilarProducts(Guid customerId)
        {
            // Chuẩn bị dữ liệu
            List<ProductInteractive> productInteractives = await GetSimilarProductInteractive();
            List<Guid> productIds = _context.Products.Select(item => item.Id).ToList();

            // Huấn luyện mô hình dự đoán sản phẩm tương tự
            MLContext mlContext = new MLContext();

            (IDataView trainingDataView, IDataView testDataView) = ProductPrediction.LoadData(mlContext, productInteractives, productInteractives);

            ITransformer model = ProductPrediction.BuildAndTrainModel(mlContext, trainingDataView);

            ProductPrediction.EvaluateModel(mlContext, testDataView, model);

            var productIdPredictions = ProductPrediction.UseModelForMultiPrediction(mlContext, model, customerId, productIds);

            // Lấy danh sách thông tin sản phẩm 
            var result = await GetProductByProductIds(productIdPredictions);

            return result;
        }
    }
}
