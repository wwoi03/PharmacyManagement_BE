using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.PaymentEcommerceFeatures.Requests;

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

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOrderCommandRequest request)
        {
            try
            {
                request.Context = HttpContext;
                var result = await _mediator.Send(request);

                /*if (result.IsSuccessed)
                {
                    if (result.Code == 200 && result.Obj != null)
                    {
                        return Redirect(result.Obj);
                    }
                }*/
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback()
        {
            try
            {
                var result = await _mediator.Send(new PaymentCallbackCommandRequest { Collections = Request.Query });
                //return Redirect("http://localhost:4200/ecommerce/checkout");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
