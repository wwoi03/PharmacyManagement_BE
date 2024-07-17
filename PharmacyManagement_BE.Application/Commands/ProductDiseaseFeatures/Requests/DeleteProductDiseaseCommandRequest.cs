using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Requests
{
    public class DeleteProductDiseaseCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid DiseaseId { get; set; }
        public Guid ProductId { get; set; }
    }
}
