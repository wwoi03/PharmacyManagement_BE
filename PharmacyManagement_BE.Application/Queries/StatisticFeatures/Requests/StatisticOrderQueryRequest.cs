using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests
{
    public class StatisticOrderQueryRequest : IRequest<ResponseAPI<List<StatisticDTO>>>
    {
        public TimeType type { get; set; } = TimeType.week;

        public bool ValidateTimeType(TimeType timeType)
        {
            return Enum.IsDefined(typeof(TimeType), timeType);
        }
    }
}
