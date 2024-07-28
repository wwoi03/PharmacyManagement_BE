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
        private readonly IPMEntities _entities;

        public StatisticQueryHandler (IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<GeneralStatisticsDTO>> Handle(StatisticQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
               
                //Kiểm tra tồn tại
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<GeneralStatisticsDTO>(StatusCodes.Status400BadRequest, validation.Message);

                // Đơn hàng
                var order = await _entities.OrderService.StatisticOrder(request.Order);

                //Doanh thu
                var revenue = await _entities.OrderService.StatisticOrder(request.Revenue);

                //Lấy danh sách yêu cầu hủy
                var listRequestCancellation = await _entities.OrderService.GetCanceledOrder();

                //Lấy danh sách cmt hỏi đáp
                var listCommentQA = await _entities.CommentService.GetCustomerCommentQANoReplys();

                //Lấy danh sách cmt đánh giá
                var listCommentEvaluate = await _entities.CommentService.GetCustomerCommentEvaluateNoReplys();

                var generalStatistics = new GeneralStatisticsDTO
                {
                    StatisticRevenue = revenue,
                    StatisticOrder = order,
                    GetCancellations = listRequestCancellation,
                    GetCustomerCommentEvaluates = listCommentEvaluate,
                    GetCustomerCommentQAs = listCommentQA
                };

                return new ResponseSuccessAPI<GeneralStatisticsDTO>(StatusCodes.Status200OK, "Thống kê", generalStatistics);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<GeneralStatisticsDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
           
        }
    }
}
