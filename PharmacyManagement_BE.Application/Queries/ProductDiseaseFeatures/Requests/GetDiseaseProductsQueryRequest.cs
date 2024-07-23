using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductDiseaseFeatures.Requests
{
    public class GetDiseaseProductsQueryRequest : IRequest<ResponseAPI<List<ProductDiseaseDTO>>>
    {
        public Guid Id { get; set; }
    }
}
