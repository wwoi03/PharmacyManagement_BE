using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
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
    public class SupportService : RepositoryService<Support>, ISupportService
    {
        public SupportService(PharmacyManagementContext context) : base(context)
        {

        }
        public async Task<ResponseAPI<string>> CheckExit(string Code, string Name, Guid? Id )
        {
            //Kiểm tra tồn tại mã code 
            var checkCode = await Context.Supports.AnyAsync(r => r.CodeSupport.ToUpper() == Code.ToUpper() && (Id == null || r.Id != Id));

            if (checkCode)
                return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, "Mã hỗ trợ của thuốc đã tồn tại, vui lòng kiểm tra lại");

            //Kiểm tra tồn tại tên
            var checkName = await Context.Supports.AnyAsync(r => r.Name.ToUpper() == Name.ToUpper() && (Id == null || r.Id != Id));

            if (checkName)
                return new ResponseErrorAPI<string>(StatusCodes.Status409Conflict, "Tên hỗ trợ của thuốc đã tồn tại, vui lòng kiểm tra lại");

            return new ResponseSuccessAPI<string>();
        }

        public async Task<List<SelectSupportDTO>> GetSupportSelect()
        {
            return Context.Supports
                 .Select(p => new SelectSupportDTO
                 {
                     Id = p.Id,
                     Name = p.Name,
                     CodeSupport = p.CodeSupport,
                 })
                 .ToList();
        }

        public async Task<List<Support>> Search(string KeyWord, CancellationToken cancellationToken)
        {
            return await Context.Supports.Where
             (d => EF.Functions.Like(d.CodeSupport.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Name.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Description.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%"))
             .ToListAsync(cancellationToken);
        }

       
    }
}
