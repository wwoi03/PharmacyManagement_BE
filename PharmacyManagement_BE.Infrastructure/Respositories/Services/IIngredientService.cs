using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IIngredientService : IRepositoryService<Ingredient>
    {
        Task<ResponseAPI<string>> CheckExit(string Code, string Name, Guid? Id = null);
        Task<List<Ingredient>> Search(string KeyWord, CancellationToken cancellationToken);
    }
}
