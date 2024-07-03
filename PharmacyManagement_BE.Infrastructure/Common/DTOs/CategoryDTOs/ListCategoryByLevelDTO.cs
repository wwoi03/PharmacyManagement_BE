using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs
{
    public class ListCategoryByLevelDTO
    {
        public Guid Id { get; set; }
        public string CodeCategory { get; set; }
        public string CategoryName { get; set; }
        public int NumberChildren { get; set; }
        public List<ListCategoryByLevelDTO> Children { get; set; }

        public ListCategoryByLevelDTO()
        {
            Children = new List<ListCategoryByLevelDTO>();
        }
    }
}
