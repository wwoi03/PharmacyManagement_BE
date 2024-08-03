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
    public class StatisticRevenueQueryRequest : IRequest<ResponseAPI<List<StatisticRevenueDTO>>>
    {
        public StatisticRevenueQueryRequest(string TimeType, DateTime dateTime)
        {
            this.TimeType = TimeType;
            this.DateTime = dateTime;
        }

        public StatisticRevenueQueryRequest() { }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public string TimeType { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (!Enum.TryParse(typeof(TimeType), TimeType, out _))
                return new ValidationNotifyError<string>("Vui lòng chọn thống kê doanh thu.");

            return new ValidationNotifySuccess<string>();

        }
    }
}

