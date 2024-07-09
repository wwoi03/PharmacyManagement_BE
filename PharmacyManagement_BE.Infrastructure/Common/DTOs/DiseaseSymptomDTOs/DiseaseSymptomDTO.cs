using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Respositories.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseSymptomDTOs
{
    public class DiseaseSymptomDTO
    {
        public Guid DiseaseId { get; set; }
        public Disease Disease { get; set; } = null!;
        public Guid SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;
    }
}
