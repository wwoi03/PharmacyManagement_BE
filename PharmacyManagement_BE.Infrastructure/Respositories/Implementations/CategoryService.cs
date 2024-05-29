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
    public class CategoryService : RepositoryService<Category>, ICategoryService
    {
        private readonly PharmacyManagementContext _context;

        public CategoryService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public List<Category> GetChildCategories(List<Category> allCategories, Guid parentId)
        {
            throw new NotImplementedException();
        }
    }
}
