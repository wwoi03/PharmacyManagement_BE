using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.IngredientDTOs;
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
    public class IngredientService : RepositoryService<Ingredient>, IIngredientService
    {
        public IngredientService(PharmacyManagementContext context) : base(context)
        {

        }

        public async Task<ResponseAPI<string>> CheckExit(string Code, string Name, Guid? Id)
        {
            //Kiểm tra tồn tại mã code 
            var checkCode = await Context.Ingredients.AnyAsync(r => r.CodeIngredient.ToUpper() == Code.ToUpper() && (Id == null || r.Id != Id));

            if (checkCode)
                return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, "Mã hỗ trợ của thuốc đã tồn tại, vui lòng kiểm tra lại");

            //Kiểm tra tồn tại tên
            var checkName = await Context.Ingredients.AnyAsync(r => r.Name.ToUpper() == Name.ToUpper() && (Id == null || r.Id != Id));

            if (checkName)
                return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, "Tên hỗ trợ của thuốc đã tồn tại, vui lòng kiểm tra lại");

            return new ResponseSuccessAPI<string>();
        }

        public async Task<List<SelectIngredientDTO>> GetIngredientSelect()
        {
            return Context.Ingredients
                .Select(p => new SelectIngredientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CodeIngredient = p.CodeIngredient,
                })
                .ToList();
        }

        public async Task<List<Ingredient>> Search(string KeyWord, CancellationToken cancellationToken)
        {
            return await Context.Ingredients.Where
             (d => EF.Functions.Like(d.CodeIngredient.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Name.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%"))
             .ToListAsync(cancellationToken);
        }
    }
}

