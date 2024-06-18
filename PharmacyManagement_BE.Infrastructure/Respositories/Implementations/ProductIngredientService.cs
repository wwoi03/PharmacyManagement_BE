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
    }
}

