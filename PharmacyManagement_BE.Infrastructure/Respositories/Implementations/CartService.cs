using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs;
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
    public class CartService : RepositoryService<Cart>, ICartService
    {
        private readonly PharmacyManagementContext _context;

        public CartService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<ItemCartDTO>> GetCart(Guid customerId)
        {
            return _context.Carts
                .Where(i => i.CustomerId == customerId)
                .Include(i => i.Product)
                .Include(i => i.Unit)
                .Select(i => new ItemCartDTO
                {
                    CartId = i.Id,
                    ProductId = i.ProductId,
                    UnitId = i.UnitId,
                    ProductName = i.Product.Name,
                    ProductImage = i.Product.Image,
                    UnitName = i.Unit.NameDetails,
                    Quantity = i.Quantity,
                })
                .ToList();
        }
    }
}
