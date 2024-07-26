using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IProductDiseaseService : IRepositoryService<ProductDisease>
    {
        Task<bool> CreateRange(List<ProductDisease> productDiseases);
        Task<ProductDisease> GetProductDisease(Guid productId, Guid diseaseId);
        Task<List<ProductDisease>> GetAllByDisease(Guid diseaseId);
        Task<ResponseAPI<string>> CheckExit(Guid? productId, Guid? diseaseId);
    }
}
