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
    internal class StatisticOrderQueryHandler : IRequestHandler<StatisticOrderQueryRequest, ResponseAPI<List<StatisticDTO>>>
    {
        private readonly IPMEntities _entities;

        public StatisticOrderQueryHandler (IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<StatisticDTO>>> Handle(StatisticOrderQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
               
                //Kiểm tra tồn tại
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<List<StatisticDTO>> (StatusCodes.Status400BadRequest, validation.Message);

                // Đơn hàng
                var order = await _entities.OrderService.StatisticOrder(request.Order);

                ////Lấy danh sách yêu cầu hủy
                //var listRequestCancellation = await _entities.OrderService.GetCanceledOrder();

                ////Lấy danh sách cmt hỏi đáp
                //var listCommentQA = await _entities.CommentService.GetCustomerCommentQANoReplys();

                ////Lấy danh sách cmt đánh giá
                //var listCommentEvaluate = await _entities.CommentService.GetCustomerCommentEvaluateNoReplys();

                //var generalStatistics = new GeneralStatisticsDTO
                //{
                //    StatisticRevenue = revenue,
                //    StatisticOrder = order,
                //    GetCancellations = listRequestCancellation,
                //    GetCustomerCommentEvaluates = listCommentEvaluate,
                //    GetCustomerCommentQAs = listCommentQA
                //};

                return new ResponseSuccessAPI<List<StatisticDTO>> (StatusCodes.Status200OK, "Thống kê", order);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<StatisticDTO>> (StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
           
        }
    }
}
