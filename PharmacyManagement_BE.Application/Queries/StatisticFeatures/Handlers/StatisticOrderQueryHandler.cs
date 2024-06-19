using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Handlers
{
    internal class StatisticOrderQueryHandler : IRequestHandler<StatisticOrderQueryRequest, ResponseAPI<List<StatisticDTO>>>
    {
        private readonly IPMEntities _entities;

        public StatisticOrderQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<StatisticDTO>>> Handle(StatisticOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var validation = request.ValidateTimeType(request.type);

                if (!validation)
                    return new ResponseErrorAPI<List<StatisticDTO>>(StatusCodes.Status400BadRequest, "Giá trị không hợp lệ.");

                var statistic = await _entities.OrderService.StatisticOrder(request.type);

                //Trả về danh sách order
                return new ResponseSuccessAPI<List<StatisticDTO>>(StatusCodes.Status200OK, "Thống kê đơn hàng", statistic);
               
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<StatisticDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
