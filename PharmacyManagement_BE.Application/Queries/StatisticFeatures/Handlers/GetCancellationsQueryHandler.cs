using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
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
    internal class GetCancellationsQueryHandler : IRequestHandler<GetCancellationsQueryRequest, ResponseAPI<List<OrderDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCancellationsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<OrderDTO>>> Handle(GetCancellationsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách cmt
                var listOrder = await _entities.OrderService.GetRequestCancellations();

                //Trả về danh sách
                return new ResponseSuccessAPI<List<OrderDTO>>(StatusCodes.Status200OK, "Danh sách yêu cầu xác nhận hủy đơn", listOrder);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<OrderDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
