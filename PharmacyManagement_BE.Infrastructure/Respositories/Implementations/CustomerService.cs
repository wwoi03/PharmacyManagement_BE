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
    public class CustomerService : RepositoryService<Customer>, ICustomerService
    {
        private readonly PharmacyManagementContext _context;

        public CustomerService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Customer?> GetCustomerByUsername(string username)
        {
            return _context.Customers.FirstOrDefault(u => u.UserName == username);
        }
    }
}
