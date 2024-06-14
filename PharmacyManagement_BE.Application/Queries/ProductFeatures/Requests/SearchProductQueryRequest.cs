using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests
{
    public class SearchProductQueryRequest : IRequest<ResponseAPI<List<ListProductDTO>>>
    {
        public string ContentStr { get; set; }
        public string CategoryName { get; set; }
    }
}
