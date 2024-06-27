using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.OrderFeatures.Requests;
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
                //Lấy danh sách đơn hàng
                var listOrder = await _entities.OrderService.GetAll();

                //Gán danh sách đơn hàng thành response
                var response = _mapper.Map<List<OrderDTO>>(listOrder);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<OrderDTO>>(StatusCodes.Status200OK, "Danh sách đơn hàng", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<OrderDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
