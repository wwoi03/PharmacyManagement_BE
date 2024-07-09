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
    public class ProductSupportService : RepositoryService<ProductSupport>, IProductSupportService
    {
        private readonly PharmacyManagementContext _context;

        public ProductSupportService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CreateRange(List<ProductSupport> productSupports)
        {
            try
            {
                await _context.ProductSupports.AddRangeAsync(productSupports);                
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ProductSupport> GetProductSupport(Guid supportId, Guid productId)
        {
            return await _context.ProductSupports.FirstOrDefaultAsync(r => r.ProductId == productId && r.SupportId == supportId);
        }

        public async Task<List<ProductSupport>> GetAllBySupport(Guid supportId)
        {
            return await _context.ProductSupports
                .Include(r => r.Product)
                .Include(r => r.Support)
                .Where(r => r.SupportId == supportId).ToListAsync();
        }
    }
}
