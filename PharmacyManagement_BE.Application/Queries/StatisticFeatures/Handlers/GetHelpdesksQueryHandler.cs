using MediatR;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Handlers
{
    internal class GetHelpdesksQueryHandler : IRequestHandler<GetHelpdesksQueryRequest, ResponseAPI<string>>
    {
        public Task<ResponseAPI<string>> Handle(GetHelpdesksQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
