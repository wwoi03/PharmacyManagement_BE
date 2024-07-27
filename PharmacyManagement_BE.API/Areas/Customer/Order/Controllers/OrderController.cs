using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.PaymentEcommerceFeatures.Requests;
using System.Web;

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

                var updatePaymentStatusOrder = await _mediator.Send(new UpdatePaymentStatusOrderCommandRequest { PaymentResponse = result.Obj });

                // Chuyển đổi result thành query parameters
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                foreach (var property in result.Obj.GetType().GetProperties())
                {
                    queryString[property.Name] = property.GetValue(result.Obj)?.ToString();
                }

                var url = "http://localhost:4200/ecommerce/checkout?" + queryString.ToString();
                HttpContext.Response.Redirect(url);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
