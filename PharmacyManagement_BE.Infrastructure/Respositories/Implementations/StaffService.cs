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
    public class StaffService : RepositoryService<Staff>, IStaffService
    {
        private readonly PharmacyManagementContext _context;

        public StaffService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Staff>> GetStaffsByBranch(Guid branchId)
        {
            return _context.Staffs.Where(s => s.BranchId == branchId).ToList();
        }
    }
}
