using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class PromotionService : RepositoryService<Promotion>, IPromotionService
    {
        private readonly IMapper _mapper;

        public PromotionService(PharmacyManagementContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> CheckExit(string Code, Guid? Id = null)
        {
            ValidationNotify<string> validation = new ValidationNotifySuccess<string>();

            int status = StatusCodes.Status200OK;

            //Kiểm tra tồn tại mã code 
            var checkCode = await Context.Promotions.AnyAsync(r => r.CodePromotion.ToUpper() == Code.ToUpper() && (Id == null || r.Id != Id));

            if (checkCode)
            {
                validation = new ValidationNotifyError<string>();
                validation.Obj = "codePromotion";
                validation.Message = "Mã khuyến mãi đã tồn tại, vui lòng kiểm tra lại";
                status = StatusCodes.Status409Conflict;
            }

            return new ResponseErrorAPI<string>(status, validation);
        }

        public async Task<Promotion> FindByCode(string code)
        {
            return await Context.Promotions.FirstOrDefaultAsync(r => r.CodePromotion == code);
        }

        public async Task<ResponseAPI<string>> DeleteRelationShip(Guid Promotion)
        {
            try
            {
                ValidationNotify<string> validation = new ValidationNotifySuccess<string>();
                int status = StatusCodes.Status200OK;

                //Lấy danh sách của PromotionProduct
                var promotionProduct = await Context.PromotionProducts.Where(r => r.PromotionId == Promotion).ToListAsync();

                if(promotionProduct.Count == 0)
                    return new ResponseSuccessAPI<string>(status, "Không có quan hệ ràng buộc");

                foreach (var item in promotionProduct)
                {
                    //Kiểm tra tồn PromotionProgram
                    var promotionProgram = await Context.PromotionPrograms.Where(r => r.PromotionProductId == item.Id).ToListAsync();
                    Context.Remove(promotionProgram);
                }

                Context.Remove(promotionProduct);

                Context.SaveChanges();

                return new ResponseSuccessAPI<string>(status, "Xóa thành công");

            }catch(Exception ex)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Xóa thất bại");
            }
        }

        public async Task<List<ProductPromotionDTO>> GetRelationShip(Guid Promotion)
        {
            List<ProductPromotionDTO> productPromotions = new List<ProductPromotionDTO>();

            // Lấy danh sách của PromotionProduct
            var promotionProduct = await Context.PromotionProducts
                .Where(r => r.PromotionId == Promotion)
                .ToListAsync();

            foreach (var item in promotionProduct)
            {
                List<PromotionProgramDTO> promotionProgramDTOs = new List<PromotionProgramDTO>();

                Product product = new Product();

                // Lấy danh sách PromotionProgram theo PromotionProductId
                var promotionPrograms = await Context.PromotionPrograms
                    .Where(r => r.PromotionProductId == item.Id)
                    .ToListAsync();


                foreach (var program in promotionPrograms)
                {
                    product = await Context.Products.FirstOrDefaultAsync(r => r.Id == program.ProductId);

                    var promotionProgramDTO = new PromotionProgramDTO
                    {
                        PromotionProductId = program.PromotionProductId,
                        ProductId = program.ProductId,
                        Quantity = program.Quantity,

                        ProductName = product.Name,
                        CodeProduct = product.CodeMedicine,
                    };

                    promotionProgramDTOs.Add(promotionProgramDTO);
                }

                //Lấy product khuyến mãi
                 product = await Context.Products.FirstOrDefaultAsync(r => r.Id == item.ProductId);

                // Tạo ProductPromotionDTO và thêm danh sách PromotionProgramDTO vào thuộc tính ProductPromotions
                var productPromotionDTO = new ProductPromotionDTO
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    PromotionId = item.PromotionId,
                    AdditionalInfo = item.AdditionalInfo,
                    Quantity = item.Quantity,

                    ProductName = product.Name,
                    CodeProduct = product.CodeMedicine,

                    PromotionPrograms = promotionProgramDTOs,
                };

                productPromotions.Add(productPromotionDTO);
            }

            return productPromotions;

        }
    }

}
