using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ISupportService : IRepositoryService<Support>
    {
        Task<bool> CheckExit(string Name, string description);
        Task<List<Support>> Search(string KeyWord, CancellationToken cancellationToken);
    }
}
