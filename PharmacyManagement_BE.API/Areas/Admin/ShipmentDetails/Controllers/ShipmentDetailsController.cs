using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.ShipmentDetails.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [ApiExplorerSettings(GroupName = "Admin")]
    public class ShipmentDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShipmentDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("GetShipmentDetailsByShipment")]
        public async Task<IActionResult> GetShipmentDetailsByShipment(GetShipmentDetailsByShipmentQueryRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
