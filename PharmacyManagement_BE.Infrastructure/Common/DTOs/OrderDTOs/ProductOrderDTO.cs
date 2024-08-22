using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductIngredientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs
{
    public class ProductOrderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeMedicine { get; set; }
        public string? Specifications { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Uses { get; set; }
        public string? HowToUse { get; set; }
        public string? SideEffects { get; set; }
        public string? Warning { get; set; }
        public string? Preserve { get; set; }
        public string? Dosage { get; set; }
        public string? Contraindication { get; set; }
        public string? DosageForms { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? BrandOrigin { get; set; }
        public string? AgeOfUse { get; set; }
        public Guid? CategoryId { get; set; }
        public string Image { get; set; }
        public List<string> Images { get; set; }
        public List<DetailsProductIngredientDTO>? ProductIngredients { get; set; }
        public List<Guid>? ProductSupports { get; set; }
        public List<Guid>? ProductDiseases { get; set; }
    }
}
