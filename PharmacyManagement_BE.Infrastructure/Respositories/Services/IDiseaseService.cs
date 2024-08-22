using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IDiseaseService : IRepositoryService<Disease>
    {
         Task<ResponseAPI<string>> CheckExit(string Code, string Name, Guid?Id = null);
         Task<List<Disease>> Search(string KeyWord, CancellationToken cancellationToken);
         Task<List<SelectDiseaseDTO>> GetDiseaseSelect();
    }
}
