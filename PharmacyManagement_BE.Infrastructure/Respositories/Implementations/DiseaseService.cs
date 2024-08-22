using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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

        public async Task<ResponseAPI<string>> CheckExit(string Code, string Name, Guid? Id )
        {
            ValidationNotify<string> validation = new ValidationNotifySuccess<string>();
            int status = StatusCodes.Status200OK;

            //Kiểm tra tồn tại mã code 
            var checkCode = await Context.Diseases.AnyAsync(r => r.CodeDisease.ToUpper() == Code.ToUpper() && (Id == null || r.Id != Id));

            if (checkCode)
            {
                validation = new ValidationNotifyError<string>();
                validation.Obj = "codeDisease";
                validation.Message = "Mã bệnh đã tồn tại, vui lòng kiểm tra lại";
                status = StatusCodes.Status409Conflict;
            }

            //Kiểm tra tồn tại tên
            var checkName = await Context.Diseases.AnyAsync(r => r.Name.ToUpper() == Name.ToUpper() && (Id == null || r.Id != Id));

            if (checkName)
            {
                validation = new ValidationNotifyError<string>();
                validation.Obj = "name";
                validation.Message = "Tên bệnh đã tồn tại, vui lòng kiểm tra lại";
                status = StatusCodes.Status409Conflict;
            }

            
            return new ResponseSuccessAPI<string>(status,validation);
        }

        public async Task<List<SelectDiseaseDTO>> GetDiseaseSelect()
        {
            return Context.Diseases
                 .Select(p => new SelectDiseaseDTO
                 {
                     Id = p.Id,
                     Name = p.Name,
                     CodeDisease = p.CodeDisease,
                 })
                 .ToList();
        }

        public async Task<List<Disease>> Search(string KeyWord, CancellationToken cancellationToken)
        {
            return await Context.Diseases.Where
             (d => EF.Functions.Like(d.CodeDisease.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Name.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%") || //<=== Hoặc nè, không thấy rồi bắt bẻ tui đi nha
             EF.Functions.Like(d.Description.ToUpper().Trim(), $"%{KeyWord.ToUpper().Trim()}%"))
             .ToListAsync(cancellationToken);
        }
        
        public async Task<Disease> FindByCode (string code)
        {
            Disease i = await Context.Diseases.FirstOrDefaultAsync(r => r.CodeDisease == code);
            return i;
        }

    }
}
