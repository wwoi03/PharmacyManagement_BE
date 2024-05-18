using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ProductDisease 
    {
        [Key]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        [Key]
        public Guid DiseaseId { get; set; }
        public Disease Disease { get; set; } = null!;
    }
}
