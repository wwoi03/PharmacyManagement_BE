using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
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

        public async Task<List<ProductSupport>> GetProductSupportsByProductId(Guid productId)
        {
            return _context.ProductSupports
                .Where(item => item.ProductId == productId)
                .Include(item => item.Support)
                .ToList();
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

        public async Task<ResponseAPI<string>> CheckExit(Guid? productId, Guid? supportId)
        {
            ValidationNotify<string> validation = new ValidationNotifySuccess<string>();
            int status = StatusCodes.Status200OK;

            //Kiểm tra tồn tại mã code 
            var checkExit = await Context.ProductSupports.AnyAsync(r => r.ProductId == productId && r.SupportId == supportId);

            if (checkExit)
            {
                validation = new ValidationNotifyError<string>();
                validation.Message = "Quan hệ đã tồn tại";
                status = StatusCodes.Status409Conflict;
            }

            return new ResponseSuccessAPI<string>(status, validation);
        }
    }
}
