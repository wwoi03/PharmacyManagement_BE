using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Account.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [ApiExplorerSettings(GroupName = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("LockAccount")]
        public async Task<IActionResult> LockAccount(LockAccountCommandRequest request)
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

        [HttpPost("UnLockAccount")]
        public async Task<IActionResult> UnLockAccount(UnLockAccountCommandRequest request)
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

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommandRequest request)
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
