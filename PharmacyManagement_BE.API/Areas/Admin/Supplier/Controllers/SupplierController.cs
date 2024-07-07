using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.SupplierFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Supplier.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator mediator;

        public SupplierController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetSupplierByCode")]
        public async Task<IActionResult> GetSupplierByCode([FromQuery] string codeSupplier)
        {
            try
            {
                var result = await mediator.Send(new GetSupplierByCodeQueryRequest {  CodeSupplier = codeSupplier });
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
