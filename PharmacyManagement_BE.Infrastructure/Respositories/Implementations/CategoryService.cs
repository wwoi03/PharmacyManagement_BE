using Dapper;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
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
        private readonly PMDapperContext _dapperContext;

        public CategoryService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._context = context;
            this._dapperContext = dapperContext;
        }

        public async Task<List<ListCategoryDTO>> GetChildrenCategories(Guid parentCategoryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ParentCategoryId", parentCategoryId);

            string sql = @"
                    SELECT 
                        parent.Id AS Id,
                        parent.CodeCategory AS CodeCategory,
                        parent.Name AS CategoryName,
	                    Count(children.Id) AS NumberChildren
                    FROM 
                        Categories parent
	                    LEFT JOIN Categories children ON children.ParentCategoryId = parent.Id
                    WHERE
                        parent.ParentCategoryId = @ParentCategoryId
                    Group by parent.Id, parent.CodeCategory, parent.Name
                    Order by parent.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ListCategoryDTO>(sql, parameters)).ToList();
        }

        public async Task<List<ListHierarchicalCategoryDTO>> GetHierarchicalCategories()
        {
            string sql = @"
                    SELECT 
                        A.Id as Level1Id,
                        A.Name as Level1CategoryName,
                        B.Id as Level2Id,
                        B.Name as Level2CategoryName,
                        C.Id as Level3Id,       
                        C.Name as Level3CategoryName
                    FROM 
                        Categories A
	                    LEFT JOIN Categories B ON B.ParentCategoryId = A.Id
	                    LEFT JOIN Categories C ON C.ParentCategoryId = B.Id
                    WHERE
                        A.ParentCategoryId IS NULL
                    Group by A.Id, A.Name, B.Id, B.Name, C.Id, C.Name
                    Order by A.Id, A.Name, B.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ListHierarchicalCategoryDTO>(sql)).ToList();
        }

        public async Task<List<ListCategoryDTO>> GetParentCategories()
        {
            string sql = @"
                    SELECT 
                        parent.Id AS Id,
                        parent.CodeCategory AS CodeCategory,
                        parent.Name AS CategoryName,
	                    Count(children.Id) AS NumberChildren
                    FROM 
                        Categories parent
	                    LEFT JOIN Categories children ON children.ParentCategoryId = parent.Id
                    WHERE
                        parent.ParentCategoryId IS NULL
                    Group by parent.Id, parent.CodeCategory, parent.Name
                    Order by parent.Name";

            return (await _dapperContext.GetConnection.QueryAsync<ListCategoryDTO>(sql)).ToList();
        }

        public async Task<List<ListCategoryDTO>> SearchCategories(string contentStr)
        {
            return _context.Categories
                .Where(i => i.Name.Contains(contentStr) || i.CodeCategory.Equals(contentStr.Trim()))
                .GroupJoin(_context.Categories,
                    parentCategory => parentCategory.Id,
                    childrenCategory => childrenCategory.ParentCategoryId,
                    (parentCategory, childrenCategory) => new { parentCategory, childrenCategory })
                .SelectMany(
                    temp => temp.childrenCategory.DefaultIfEmpty(), // Xử lý trường hợp không có childrenCategory nào
                    (parent, child) => new { parent.parentCategory, child })
                .GroupBy(
                    temp => new { temp.parentCategory.Id, temp.parentCategory.CodeCategory, temp.parentCategory.Name },
                    temp => temp.child)
                .OrderBy(g => g.Key.Name)
                .Select(g => new ListCategoryDTO
                {
                    Id = g.Key.Id,
                    CodeCategory = g.Key.CodeCategory,
                    CategoryName = g.Key.Name,
                    NumberChildren = g.Count(x => x != null)  // Chỉ đếm những childrenCategory không phải là null
                 })
                .ToList();
        }
    }
}
