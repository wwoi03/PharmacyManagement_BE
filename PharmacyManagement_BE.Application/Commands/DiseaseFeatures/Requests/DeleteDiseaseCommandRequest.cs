using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests
{
    public class DeleteDiseaseCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
    }
}
