using MediatR;
using PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Handlers
{
    internal class SearchProductQueryHandler : IRequestHandler<SearchProductQueryRequest, ResponseAPI<List<string>>>
    {
        public SearchProductQueryHandler()
        {

        }

        public Task<ResponseAPI<List<string>>> Handle(SearchProductQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
