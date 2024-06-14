using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
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
    public class ProductService : RepositoryService<Product>, IProductService
    {
        private readonly PharmacyManagementContext _context;

        public ProductService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
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
        #endregion EF & LinQ
    }
}