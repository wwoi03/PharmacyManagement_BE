using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ICustomerService : IRepositoryService<Customer>
    {
        Task<Customer> GetCustomerByUsername(string username);
        Task<Customer> Login(string username, string password);
    }
}
