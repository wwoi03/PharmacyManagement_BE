using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.DTOs.Requests;

namespace PharmacyManagement_BE.API.Areas.Config.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Config")]
    public class ConfigController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _mediator.Send(new RefreshTokenRequest
                {
                    AccessToken = await HttpContext.GetTokenAsync("access_token")
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
