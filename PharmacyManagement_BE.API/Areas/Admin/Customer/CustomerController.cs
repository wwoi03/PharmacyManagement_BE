using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;

namespace PharmacyManagement_BE.API.Areas.Admin.Customer
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /*[HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest request)
        {
            return Ok();
        }*/
    }
}
