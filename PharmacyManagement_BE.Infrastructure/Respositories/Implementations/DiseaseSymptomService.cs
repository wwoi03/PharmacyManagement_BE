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
    }
}

