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
    public class StatisticOrderQueryRequest : IRequest<ResponseAPI<List<StatisticOrderDTO>>>
    {
        public StatisticOrderQueryRequest() { }

        public StatisticOrderQueryRequest(string TimeType, DateTime dateTime)
        {
            this.TimeType = TimeType;
            this.DateTime = dateTime;
        }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public string TimeType { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (!Enum.TryParse(typeof(TimeType), TimeType, out _))
                return new ValidationNotifyError<string>("Vui lòng chọn thống kê đơn hàng.");

            return new ValidationNotifySuccess<string>();

        }
    }
}
