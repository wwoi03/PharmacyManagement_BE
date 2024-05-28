using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagement_BE.API.Areas.Admin.Category.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /*[HttpGet("GetStaffs")]
        [Authorize(Policy = "EmployeeManager")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _mediator.Send(new GetStaffsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
