using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests;
using PharmacyManagement_BE.Domain.Types;

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

        [HttpGet("Order")]
        public async Task<IActionResult> StatisticOrder([FromQuery] StatisticOrderQueryRequest request)
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

        [HttpGet("Revenue")]
        public async Task<IActionResult> StatisticRevenue([FromQuery] StatisticRevenueQueryRequest request)
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

        [HttpGet("General")]
        public async Task<IActionResult> General()
        {
            try
            {
                var result = await _mediator.Send(new GeneralStatisticQueryRequest() );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Top10Cancellation")]
        public async Task<IActionResult> Top10Cancellation()
        {
            try
            {
                var result = await _mediator.Send(new Top10CancellationProductQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Top10Sold")]
        public async Task<IActionResult> Top10Sold( )
        {
            try
            {
                var result = await _mediator.Send(new Top10SoldProductQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Top10View")]
        public async Task<IActionResult> Top10View()
        {
            try
            {
                var result = await _mediator.Send(new Top10ViewProductQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
