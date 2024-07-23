using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.OrderFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.OrderFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;

namespace PharmacyManagement_BE.API.Areas.Admin.Order.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> Update(UpdateStatusOrderCommandRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DetailsOrder")]
        public async Task<IActionResult> Details([FromQuery] GetDetailsOrderQueryRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var result = await _mediator.Send(new GetOrdersQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Lấy trạng thái đơn hàng
        [HttpGet("GetOrderStatuses")]
        public IActionResult GetOrderStatusEnum()
        {
            var values = Enum.GetNames(typeof(OrderType));
            return Ok(values);
        }

    }
}
