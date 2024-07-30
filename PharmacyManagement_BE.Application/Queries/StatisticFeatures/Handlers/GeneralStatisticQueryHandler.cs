using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
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
    internal class GeneralStatisticQueryHandler : IRequestHandler<GeneralStatisticQueryRequest, ResponseAPI<GeneralStatisticsDTO>>
    {
        private readonly IPMEntities _entities;

        public GeneralStatisticQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<GeneralStatisticsDTO>> Handle(GeneralStatisticQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var statistic = await _entities.OrderService.RealTimeStatistc();

                //Kiểm tra get danh sách
                if (statistic == null)
                    return new ResponseErrorAPI<GeneralStatisticsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                return new ResponseSuccessAPI<GeneralStatisticsDTO>(StatusCodes.Status200OK, "Top 10 bán chạy", statistic);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<GeneralStatisticsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
