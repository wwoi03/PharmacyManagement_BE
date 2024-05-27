using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.DTOs.Requests;

namespace PharmacyManagement_BE.API.Areas.Customer.Account.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
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
    }
}
