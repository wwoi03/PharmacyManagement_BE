using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs
{
    public class DetailsCategoryDTO
    {
        public Guid Id { get; set; }
        public string CodeCategory { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public int NumberChildren { get; set; }
        public List<ListCategoryDTO>? ChildrenCategories { get; set; }
    }
}
