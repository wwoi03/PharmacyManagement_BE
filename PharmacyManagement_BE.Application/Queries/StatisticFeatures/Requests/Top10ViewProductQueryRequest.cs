using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests
{
    public class Top10ViewProductQueryRequest: IRequest<ResponseAPI<List<StatisticProductDTO>>>
    {
    }
}
