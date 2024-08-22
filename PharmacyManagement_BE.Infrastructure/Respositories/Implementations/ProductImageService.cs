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
    public class ProductImageService : RepositoryService<ProductImage>, IProductImageService
    {
        private readonly PharmacyManagementContext _context;

        public ProductImageService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ProductImage?> GetProductImagesByImage(string image)
        {
            return _context.ProductImages
                .FirstOrDefault(x => x.Image.Equals(image));
        }

        public async Task<List<ProductImage>> GetProductImagesByProductId(Guid productId)
        {
            return _context.ProductImages
                .Where(x => x.ProductId == productId)
                .ToList();
        }
    }
}

