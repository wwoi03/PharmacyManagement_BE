using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Entities
{
    public class DiseaseSymptom 
    {
        [Key]
        public Guid DiseaseId { get; set; }
        public Disease Disease { get; set; } = null!;
        [Key]
        public Guid SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;
    }
}
