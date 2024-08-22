using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class ProductIngredientService : RepositoryService<ProductIngredient>, IProductIngredientService
    {
        private readonly PharmacyManagementContext _context;

        public ProductIngredientService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CreateRange(List<ProductIngredient> productIngredients)
        {
            try
            {
                _context.ProductIngredients.AddRange(productIngredients);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductIngredient>> GetProductIngredientByProductId(Guid productId)
        {
            return _context.ProductIngredients
                .Where(item => item.ProductId == productId)
                .Include(item => item.Unit)
                .ToList();
        }

        /*public Task<bool> UpdateProductIngredientOldNew(Guid productId, List<Guid> ingredientIdNews)
        {
            var productIngredientOld = _context.ProductIngredients
                .Where(_ => _.ProductId == productId)
                .Select(item => item.IngredientId)
                .ToList();

            // Tìm các phần tử cần thêm vào 
            var itemsToAdd = ingredientIdNews.Except(productIngredientOld).ToList();

            // Tìm các phần tử cần xóa khỏi 
            var itemsToRemove = productIngredientOld.Except(ingredientIdNews).ToList();

            // Thêm thành phần sản phẩm
            if (itemsToAdd != null && itemsToAdd.Count > 0)
            {
                var productIngredients = itemsToAdd.Select(item => new ProductIngredient
                {
                    ProductId = product.Id,
                    IngredientId = item,
                    UnitId = Guid.Parse("4881154e-4715-485a-8c5c-c088295369c3"),
                    Content = 20
                }).ToList();

                var createProductIngredientsResult = await _entities.ProductIngredientService.CreateRange(productIngredients);

                if (!createProductIngredientsResult)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, $"Lỗi trong quá trình thêm thành phần sản phẩm.");
            }

            // Xóa thành phần sản phẩm
            foreach (var id in itemsToRemove)
            {
                var item = await _entities.ProductIngredientService.GetById(id);
                _entities.ProductIngredientService.Delete(item);
            }
        }*/
    }
}

