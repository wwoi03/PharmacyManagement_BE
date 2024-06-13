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
    public class SupportService : RepositoryService<Support>, ISupportService
    {
        public SupportService(PharmacyManagementContext context) : base(context)
        {

        }
        public async Task<bool> CheckExit(string name, string description)
        {
            return await Context.Supports.AnyAsync
                (r => r.Name.ToUpper().Replace(" ", "") == name.ToUpper().Replace(" ", "") &&
                r.Description.ToUpper().Replace(" ", "") == description.ToUpper().Replace(" ", ""));
        }

        public async Task<List<Support>> Search(string KeyWord, CancellationToken cancellationToken)
        {
            return await Context.Supports.Where
             (d => EF.Functions.Like(d.Name.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Description.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%"))
             .ToListAsync(cancellationToken);
        }

       
    }
}
