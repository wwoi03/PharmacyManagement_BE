using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs
{
    public class ListHierarchicalCategoryDTO
    {
        public Guid Level1Id { get; set; }
        public string Level1CategoryName { get; set; }

        public Guid Level2Id { get; set; }
        public string Level2CategoryName { get; set; }

        public Guid Level3Id { get; set; }
        public string Level3CategoryName { get; set; }
    }
}