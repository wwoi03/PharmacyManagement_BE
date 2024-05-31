using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Shiment.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> GetShipmentsByBranch(GetShipmentsByBranchQueryRequest request)
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
