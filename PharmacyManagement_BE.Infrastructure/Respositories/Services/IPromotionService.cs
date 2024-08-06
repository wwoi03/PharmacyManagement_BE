using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PromotionDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IPromotionService : IRepositoryService<Promotion>
    {
        Task<ResponseAPI<string>> CheckExit(string Code, Guid? Id = null);
        Task<Promotion> FindByCode(string code);
        Task<ResponseAPI<string>> DeleteRelationShip(Guid Promotion);
        Task<List<ProductPromotionDTO>> GetRelationShip(Guid Promotion);

    }
}
