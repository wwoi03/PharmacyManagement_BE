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
    public class Ingredient : BaseEntity<Guid>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CodeIngredient { get; set; }
    }
}
