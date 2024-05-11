using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    internal interface IProductService
    {
        Task<ResponseAPI<string>> Create(Product product);
        Task<ResponseAPI<string>> Update(Product product);
        Task<ResponseAPI<string>> Delete(Guid id);
    }
}
