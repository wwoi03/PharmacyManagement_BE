using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Branch : BaseEntity<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Chỉnh sửa để không cho phép giá trị null

        [Required]
        [StringLength(50)]
        public string ProvinceOrCity { get; set; } // Chỉnh sửa để không cho phép giá trị null

        [Required]
        [StringLength(50)]
        public string District { get; set; } // Chỉnh sửa để không cho phép giá trị null

        [Required]
        [StringLength(50)]
        public string Ward { get; set; } // Chỉnh sửa để không cho phép giá trị null

        [Required]
        [StringLength(1000)]
        public string AddressDetails { get; set; } // Chỉnh sửa để không cho phép giá trị null

        [Required]
        [StringLength(50)]
        public string Phone { get; set; } // Chỉnh sửa để không cho phép giá trị null

        public Guid? StaffId { get; set; } // Chỉnh sửa để không cho phép giá trị null
    }
}
