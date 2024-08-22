using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests
{
    public class CreateDiseaseSymptomCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid SymptomId { get; set; }
        public Guid DiseaseId { get; set; }
    }
}
