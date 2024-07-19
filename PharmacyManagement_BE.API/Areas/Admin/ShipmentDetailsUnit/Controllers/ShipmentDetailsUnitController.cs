using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsUnitFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.ShipmentDetailsUnit.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class ShipmentDetailsUnitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipmentDetailsUnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetShipmentDetailsUnitBestest")]
        public async Task<IActionResult> GetShipmentDetailsUnitBestest([FromQuery] Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new GetShipmentDetailsUnitBestestQueryRequest { ProductId = productId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
