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
    public class UnitService : RepositoryService<Unit>, IUnitService
    {
        private readonly PharmacyManagementContext _context;

        public UnitService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Unit?> GetUnitByNameOrCode(string name = "", string code = "")
        {
            return _context.Units.FirstOrDefault(i => i.NameDetails.Equals(name) || i.Name.Equals(code));
        }
    }
}
