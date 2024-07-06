using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Shiment.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class ShipmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShipmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /*[HttpPost("GetShipmentsByBranch")]
        public async Task<IActionResult> GetShipmentsByBranch(Guid branchId)
        {
            try
            {
                var result = await mediator.Send(new GetShipmentsByBranchQueryRequest { BranchId = branchId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [HttpGet("GetShipmentsByBranch")]
        public async Task<IActionResult> GetShipmentsByBranch()
        {
            try
            {
                var result = await mediator.Send(new GetShipmentsByBranchQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SearchShipments")]
        public async Task<IActionResult> SearchShipments(SearchShipmentsQueryRequest request)
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

        [HttpPost("GetCostStatisticShipment")]
        public async Task<IActionResult> GetCostStatisticShipment(GetCostStatisticsShipmentQueryRequest request)
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid shipmentId)
        {
            try
            {
                var result = await mediator.Send(new DeleteShipmentCommandRequest { ShipmentId = shipmentId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateShipmentCommandRequest request)
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
        public async Task<IActionResult> Create(CreateShipmentCommandRequest request)
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
