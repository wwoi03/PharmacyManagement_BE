using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    internal class ProductService : IProductService
    {
        public Task<ResponseAPI<string>> Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseAPI<string>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseAPI<string>> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
