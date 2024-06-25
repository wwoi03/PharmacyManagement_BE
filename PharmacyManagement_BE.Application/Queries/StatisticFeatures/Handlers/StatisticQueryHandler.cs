using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests;
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
    internal class StatisticQueryHandler : IRequestHandler<StatisticQueryRequest, ResponseAPI<GeneralStatisticsDTO>>
    {
        private readonly IMediator _mediator;

        public StatisticQueryHandler (IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task<ResponseAPI<GeneralStatisticsDTO>> Handle(StatisticQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var statisticOrderTask = _mediator.Send(new StatisticOrderQueryRequest(request.Order));
                var statisticRevenueTask = _mediator.Send(new StatisticRevenueQueryRequest(request.Revenue));
                var getCancellationsTask = _mediator.Send(new GetCancellationsQueryRequest());
                var getCustomerCommentQAsTask = _mediator.Send(new GetCustomerCommentQAsQueryRequest());
                var getCustomerCommentEvaluatesTask = _mediator.Send(new GetCustomerCommentEvaluatesRequest());


                // Chờ tất cả các task hoàn thành bất đồng bộ
                await Task.WhenAll(statisticOrderTask, statisticRevenueTask, getCancellationsTask, getCustomerCommentQAsTask, getCustomerCommentEvaluatesTask);

                // Lấy kết quả từ các task
                var generalStatistics = new GeneralStatisticsDTO
                {
                    StatisticOrder = await statisticOrderTask,
                    StatisticRevenue = await statisticRevenueTask,
                    GetCancellations = await getCancellationsTask,
                    GetCustomerCommentQAs = await getCustomerCommentQAsTask,
                    GetCustomerCommentEvaluates = await getCustomerCommentEvaluatesTask
                };

                //Kiểm tra lấy dữ liệu
                if(generalStatistics == null)
                    return new ResponseErrorAPI<GeneralStatisticsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");

                return new ResponseSuccessAPI<GeneralStatisticsDTO>(StatusCodes.Status200OK, "Thống kê", generalStatistics);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<GeneralStatisticsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
           
        }
    }
}
