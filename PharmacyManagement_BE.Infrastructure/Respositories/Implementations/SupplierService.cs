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
    public class SupplierService : RepositoryService<Supplier>, ISupplierService
    {
        private readonly PharmacyManagementContext _context;

        public SupplierService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Supplier?> GetSupplierByCode(string codeSupplier)
        {
            return _context.Suppliers.FirstOrDefault(s => s.CodeSupplier.Equals(codeSupplier));
        }
    }
}
