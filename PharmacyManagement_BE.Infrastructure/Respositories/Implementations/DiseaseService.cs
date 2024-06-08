using MediatR;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class DiseaseService : RepositoryService<Disease>, IDiseaseService
    {
        // Check: Kiểm tra tính đúng đắn
        //Find: tìm chính xác trả về 1
        //Search: Tìm gần đúng trả về list

        public DiseaseService(PharmacyManagementContext context) : base(context)
        {

        }

        public async Task<bool> CheckExit(string name, string description)
        {
            return await Context.Symptoms.AnyAsync
                (r => r.Name.ToUpper().Trim() == name.ToUpper().Trim() &&
                r.Description.ToUpper().Trim() == description.ToUpper().Trim());
        }
        public async Task<List<Disease>> SearchDisease(string KeyWord, CancellationToken cancellationToken)
        {
             return await Context.Diseases.Where
             (d => EF.Functions.Like(d.Name.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Description.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%"))
             .ToListAsync(cancellationToken);
        }
           

    }
}
