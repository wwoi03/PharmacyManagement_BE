using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.PromotionFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.PromotionFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Promotion.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class PromotionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("CreatePromotion")]
        public async Task<IActionResult> Create(CreatePromotionCommandRequest request)
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


        [HttpDelete("DeletePromotion")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            try
            {
                var result = await _mediator.Send(new DeletePromotionCommandRequest(Id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DetailsPromotion")]
        public async Task<IActionResult> Details([FromQuery] Guid Íd)
        {
            try
            {
                var result = await _mediator.Send(new GetDetailsPromotionQueryRequest(Íd));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePromotion")]
        public async Task<IActionResult> Update(UpdatePromotionCommandRequest request)
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

        [HttpGet("GetPromotions")]
        public async Task<IActionResult> GetPromotions()
        {
            try
            {
                var result = await _mediator.Send(new GetPromotionsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
