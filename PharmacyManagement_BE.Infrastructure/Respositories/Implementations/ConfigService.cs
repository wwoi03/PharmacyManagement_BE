using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class ConfigService : IConfigService
    {
        public Task<ResponseAPI<string>> UpdateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
