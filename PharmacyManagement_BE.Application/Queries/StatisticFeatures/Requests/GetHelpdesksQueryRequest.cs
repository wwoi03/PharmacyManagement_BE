using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests
{
    public class GetHelpdesksQueryRequest : IRequest<ResponseAPI<string>>
    {
    }
}
