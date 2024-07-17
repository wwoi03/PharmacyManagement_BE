using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ICategoryService : IRepositoryService<Category>
    {
        Task<List<ListCategoryDTO>> GetChildrenCategories(Guid parentCategoryId);
        Task<List<ListCategoryDTO>> GetParentCategories();
        Task<List<ListCategoryDTO>> SearchCategories(string contentStr);
        Task<List<ListHierarchicalCategoryDTO>> GetHierarchicalCategories();
        Task<DetailsCategoryDTO?> GetCategoryDetails(Guid categoryId);
        Task<Category?> GetCategoryByNameOrCode(string name, string codeCategory);
        Task<Category?> GetCategoryByName(string name);
        Task<Category?> GetCategoryByCode(string code);
        Task<List<ListCategoryByLevelDTO>> GetCategoriesByLevel();
        Task<List<SelectCategoryDTO>> GetCategoriesSelect();
    }
}
