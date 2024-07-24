using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitEcommerceDTOs;
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
            var carts = _context.Carts
                .Where(i => i.CustomerId == customerId)
                .Include(i => i.Product)
                .Include(i => i.Unit)
                .ToList();

            var cartDtos = carts.Select(i => new ItemCartDTO
            {
                CartId = i.Id,
                ProductId = i.ProductId,
                UnitId = i.UnitId,
                ProductName = i.Product.Name,
                ProductImage = i.Product.Image,
                UnitName = i.Unit.NameDetails,
                Quantity = i.Quantity,
                ShipmentDetailsUnits = _context.ShipmentDetailsUnit
                    .Where(sdu => sdu.ShipmentDetailsId == _context.ShipmentDetails
                        .Where(sd => sd.ProductId == i.ProductId)
                        .OrderByDescending(sd => sd.ImportPrice)
                        .Select(sd => sd.Id)
                        .FirstOrDefault())
                    .Select(sdu => new ShipmentDetailsUnitDTO
                    {
                        UnitId = sdu.Unit.Id,
                        CodeUnit = sdu.Unit.Name,
                        UnitName = sdu.Unit.NameDetails,
                        SalePrice = sdu.SalePrice,
                        UnitCount = sdu.UnitCount,
                        Level = sdu.Level
                    })
                    .OrderBy(sdu => sdu.Level)
                    .ToList()
            }).ToList();

            return cartDtos;
        }
    }
}
