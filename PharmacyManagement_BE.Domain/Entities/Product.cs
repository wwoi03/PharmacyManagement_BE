using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string CodeMedicine { get; set; }

        [StringLength(50)]
        public string? Specifications { get; set; }

        [StringLength(1000)]
        public string? ShortDescription { get; set; } 
        public string? Description { get; set; } 
        public string? Uses { get; set; } 
        public string? HowToUse { get; set; } 
        public string? SideEffects { get; set; } 
        public string? Warning { get; set; } 
        public string? Preserve { get; set; } 
        public string? Dosage { get; set; } 
        public string? Contraindication { get; set; }

        [StringLength(50)]
        public string? DosageForms { get; set; }

        [StringLength(50)]
        public string? RegistrationNumber { get; set; }

        [StringLength(50)]
        public string? BrandOrigin { get; set; }

        [StringLength(50)]
        public string? AgeOfUse { get; set; }
        public int View { get; set; }
        public int CartView { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Required]
        public string Image { get; set; }
    }
}
