using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.OrderFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.OrderFeatures.Handlers
{
    internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQueryRequest, ResponseAPI<List<OrderDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<OrderDTO>>> Handle(GetOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<List<OrderDTO>>(StatusCodes.Status400BadRequest, validation.Message);

                //Lấy Branch của nhân viên
                var branch = await _entities.AccountService.GetBranchId();
                

                //Lấy danh sách đơn hàng
                var listOrder = await _entities.OrderService.GetOrdersByBranch(branch, (OrderType)request.Type);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<OrderDTO>>(StatusCodes.Status200OK, "Danh sách đơn hàng", listOrder);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<OrderDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
