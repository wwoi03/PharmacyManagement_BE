using PharmacyManagement_BE.Domain.Entities.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string CodeMedicine { get; set; } = string.Empty;
        public string Specifications { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Uses { get; set; } = string.Empty;
        public string HowToUse { get; set; } = string.Empty;
        public string SideEffects { get; set; } = string.Empty;
        public string Warning { get; set; } = string.Empty;
        public string Preserve { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Contraindication { get; set; } = string.Empty;
        public string View { get; set; } = string.Empty;
        public string DosageForms { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string BrandOrigin { get; set; } = string.Empty;
        public string AgeOfUse { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
