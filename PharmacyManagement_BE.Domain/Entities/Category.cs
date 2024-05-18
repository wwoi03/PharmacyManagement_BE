using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string? Name { get; set; } 
        public Guid ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; } = null!;
    }
}
