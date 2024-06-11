using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests;
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

        [HttpGet("GetShipmentDetailsByShipment")]
        public async Task<IActionResult> GetShipmentDetailsByShipment([FromQuery] Guid shipmentId)
        {
            try
            {
                var result = await mediator.Send(new GetShipmentDetailsByShipmentQueryRequest { ShipmentId = shipmentId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid shipmentDetailsId)
        {
            try
            {
                var result = await mediator.Send(new DeleteShipmentDetailsCommandRequest { ShipmentDetailsId = shipmentDetailsId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateShipmentDetailsCommandRequest request)
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

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateShipmentDetailsCommandRequest request)
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

        [HttpPost("SearchShipmentDetailsByProduct")]
        public async Task<IActionResult> SearchShipmentDetailsByProduct(SearchShipmentDetailsByProductQueryRequest request)
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

        [HttpGet("GetDetailsShipmentDetails")]
        public async Task<IActionResult> GetDetailsShipmentDetails([FromQuery] Guid shipmentDetailsId)
        {
            try
            {
                var result = await mediator.Send(new GetDetailsShipmentDetailsQueryRequest { ShipmentDetailsId = shipmentDetailsId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
