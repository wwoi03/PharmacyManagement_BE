using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SearchEcommerceFeatures.Requests
{
    public class SearchProductQueryRequest : IRequest<ResponseAPI<List<ItemProductDTO>>>
    {
        public string Content { get; set; } = string.Empty;
        public List<Guid>? Categories { get; set; }
        public List<Guid>? Diseases { get; set; }
        public List<Guid>? Symptoms { get; set; }
        public List<Guid>? Supports { get; set; }
    }
}
