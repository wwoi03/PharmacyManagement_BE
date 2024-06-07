using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IAccountService
    {
        Task<Guid> GetAccountId();
        Task<Guid> GetBranchId();
    }
}
