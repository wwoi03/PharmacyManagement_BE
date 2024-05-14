using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class ProductIngredient
    {
        [Key]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Key]
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Content { get; set; }
    }
}
