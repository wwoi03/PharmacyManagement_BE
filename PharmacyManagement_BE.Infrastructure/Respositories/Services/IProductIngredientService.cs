using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IProductIngredientService : IRepositoryService<ProductIngredient>
    {
        Task<bool> CreateRange(List<ProductIngredient> productIngredients);
        Task<List<ProductIngredient>> GetProductIngredientByProductId(Guid productId);
       /* Task<bool> UpdateProductIngredientOldNew(Guid productId, List<Guid> ingredientOlds, List<Guid> ingredientIdNews);*/
    }
}
