using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests
{
    public class StatisticQueryRequest : IRequest<ResponseAPI<GeneralStatisticsDTO>>
    {
        public TimeType Order { get; set; } = TimeType.week;
        public TimeType Revenue { get; set; } = TimeType.week;

        public ValidationNotify<string> IsValid(int Order, int Revenue)
        {
            if (!Enum.IsDefined(typeof(TimeType), Order.GetType))
                return new ValidationNotifyError<string>("Vui lòng chọn thống kê đơn hàng.");

            if (!Enum.IsDefined(typeof(TimeType), Revenue.GetType))
                return new ValidationNotifyError<string>("Vui lòng chọn thống kê doanh thu.");

            return new ValidationNotifySuccess<string>();

        }
    }
}
