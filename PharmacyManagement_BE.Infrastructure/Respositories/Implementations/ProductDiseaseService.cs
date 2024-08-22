using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDiseaseDTOs;
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

        public async Task<List<ProductDisease>> GetProductDiseasesByProductId(Guid productId)
        {
            return _context.ProductDiseases
                .Where(item => item.ProductId == productId)
                .Include(item => item.Disease)
                .ToList();
        }

        public async Task<ProductDisease> GetProductDisease(Guid productId, Guid diseaseId)
        {
            return await _context.ProductDiseases.FirstOrDefaultAsync(r => r.ProductId == productId && r.DiseaseId == diseaseId);      
        }

        public async Task<List<ProductDisease>> GetAllByDisease(Guid diseaseId)
        {
            return await _context.ProductDiseases
                .Include(r => r.Product)
                .Include(r => r.Disease)
                .Where(r => r.DiseaseId == diseaseId).ToListAsync();
        }
        public async Task<ResponseAPI<string>> CheckExit(Guid? productId, Guid? diseaseId)
        {
            ValidationNotify<string> validation = new ValidationNotifySuccess<string>();
            int status = StatusCodes.Status200OK;

            //Kiểm tra tồn tại mã code 
            var checkExit = await Context.ProductDiseases.AnyAsync(r => r.ProductId == productId && r.DiseaseId == diseaseId);

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


