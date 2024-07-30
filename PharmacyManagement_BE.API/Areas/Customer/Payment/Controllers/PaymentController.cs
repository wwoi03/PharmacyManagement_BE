using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.PaymentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.PaymenDTOs;

namespace PharmacyManagement_BE.API.Areas.Customer.Payment.Controllers
{
    [ApiExplorerSettings(GroupName = "Customer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(PaymentInformationModel model)
        {
            try
            {
                var result = await _mediator.Send(new CreatePaymentUrlCommandRequest { Model = model, Context = HttpContext });
                return Ok(result.Obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}