using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductIngredientDTOs
{
    public class DetailsProductIngredientDTO
    {
        public Guid ProductId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string CodeIngredient { get; set; }
        public string Content { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
