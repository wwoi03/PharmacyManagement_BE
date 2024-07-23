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
    public class DiseaseSymptomService : RepositoryService<DiseaseSymptom>, IDiseaseSymptomService
    {
        public DiseaseSymptomService(PharmacyManagementContext context) : base(context)
        {

        }

        public async Task<List<DiseaseSymptom>> GetAllByDisease(Guid diseaseId)
        {
            return await Context.DiseaseSymptoms
                .Include(r => r.Disease)
                .Include(r => r.Symptom)
                .Where(r => r.DiseaseId == diseaseId).ToListAsync();
        }

        public async Task<List<DiseaseSymptom>> GetAllBySymptom(Guid symptomId)
        {
            return await Context.DiseaseSymptoms
                .Include(r => r.Symptom)
                .Include(r => r.Disease)
                .Where(r => r.SymptomId == symptomId).ToListAsync();
        }

        public async Task<DiseaseSymptom> GetDiseaseSymptom(Guid symptomId, Guid diseaseId)
        {
            return await Context.DiseaseSymptoms.FirstOrDefaultAsync(r => r.SymptomId == symptomId && r.DiseaseId == diseaseId);
        }

        public async Task<ResponseAPI<string>> CheckExit(Guid diseaseId, Guid symptomId)
        {
            ValidationNotify<string> validation = new ValidationNotifySuccess<string>();
            int status = StatusCodes.Status200OK;

            //Kiểm tra tồn tại mã code 
            var checkExit = await Context.DiseaseSymptoms.AnyAsync(r => r.DiseaseId == diseaseId && r.SymptomId == symptomId);

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

