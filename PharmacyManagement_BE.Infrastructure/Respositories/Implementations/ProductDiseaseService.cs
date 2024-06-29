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
    public class ProductDiseaseService : RepositoryService<ProductDisease>, IProductDiseaseService
    {
        private readonly PharmacyManagementContext _context;

        public ProductDiseaseService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CreateRange(List<ProductDisease> productDiseases)
        {
            try
            {
                await _context.ProductDiseases.AddRangeAsync(productDiseases);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


