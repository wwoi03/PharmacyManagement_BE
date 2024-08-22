using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Handlers
{
    internal class StatisticRevenueQueryHandler : IRequestHandler<StatisticRevenueQueryRequest, ResponseAPI<List<StatisticRevenueDTO>>>
    {
        private readonly IPMEntities _entities;

        public StatisticRevenueQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<StatisticRevenueDTO>>> Handle(StatisticRevenueQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                //Kiểm tra tồn tại
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<List<StatisticRevenueDTO>>(StatusCodes.Status400BadRequest, validation.Message);

                //Doanh thu
                var revenue = await _entities.OrderService.StatisticRevenue((TimeType)Enum.Parse(typeof(TimeType), request.TimeType), request.DateTime);

                return new ResponseSuccessAPI<List<StatisticRevenueDTO>>(StatusCodes.Status200OK, "Thống kê", revenue);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<StatisticRevenueDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }

        }
    }
}
