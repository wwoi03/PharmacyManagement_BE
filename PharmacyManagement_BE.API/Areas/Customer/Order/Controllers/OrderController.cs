using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Customer.Order.Controllers
{
    [ApiExplorerSettings(GroupName = "Customer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            try
            {
                request.Context = HttpContext;
                var result = await _mediator.Send(request);
                return Ok(result.Obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
