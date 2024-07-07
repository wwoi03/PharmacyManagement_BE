using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Statistic.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class StatisticController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("Statistic")]
        public async Task<IActionResult> Statistic([FromQuery] StatisticQueryRequest request)
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
