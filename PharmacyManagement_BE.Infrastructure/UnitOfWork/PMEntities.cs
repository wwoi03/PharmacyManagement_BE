using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.UnitOfWork
{
    public class PMEntities : IPMEntities
    {
        private PharmacyManagementContext _context;
        public IProductService ProductService { get; set; }

        public PMEntities(PharmacyManagementContext context, IProductService productService)
        {
            this._context = context;
            this.ProductService = productService;
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }
}
