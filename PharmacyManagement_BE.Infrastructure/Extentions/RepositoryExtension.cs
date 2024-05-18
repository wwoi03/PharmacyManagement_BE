using Microsoft.Extensions.DependencyInjection;
using PharmacyManagement_BE.Infrastructure.Respositories.Implementations;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Extentions
{
    public static class RepositoryExtension
    {
        public static void AddRepositoryExtension(this IServiceCollection services)
        {
            services.AddTransient<IPMEntities, PMEntities>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
