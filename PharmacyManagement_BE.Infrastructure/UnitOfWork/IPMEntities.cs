using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.UnitOfWork
{
    public interface IPMEntities
    {
        IProductService ProductService { get; set; }
        int SaveChange();
    }
}
