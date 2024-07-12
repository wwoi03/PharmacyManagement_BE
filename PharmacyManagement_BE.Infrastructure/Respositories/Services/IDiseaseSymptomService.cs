using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IDiseaseSymptomService : IRepositoryService<DiseaseSymptom>
    {
        Task<DiseaseSymptom> GetDiseaseSymptom(Guid symptomId, Guid diseaseId);
        Task<List<DiseaseSymptom>> GetAllByDisease(Guid diseaseId);
        Task<List<DiseaseSymptom>> GetAllBySymptom(Guid symptomId);
        Task<ResponseAPI<string>> CheckExit(Guid diseaseId, Guid symptomId);
    }
}
