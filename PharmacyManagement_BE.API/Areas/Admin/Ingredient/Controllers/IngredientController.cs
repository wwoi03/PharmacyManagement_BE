using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.IngredientFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.IngredientFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Ingredient.Controller
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        //Tạo thành phần mới
        [HttpPost("CreateIngredient")]
        public async Task<IActionResult> Create(CreateIngredientCommandRequest request)
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

        //Cập nhật thành phần 
        [HttpPut("UpdateIngredient")]
        public async Task<IActionResult> Update(UpdateIngredientCommandRequest request)
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

        [HttpDelete("DeleteIngredient")]
        public async Task<IActionResult> Delete(DeleteIngredientCommandRequest request)
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

        [HttpGet("DetailsIngredient")]
        public async Task<IActionResult> Details([FromQuery] GetDetailsIngredientQueryRequest request)
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

        [HttpGet("GetIngredients")]
        public async Task<IActionResult> GetIngredients()
        {
            try
            {
                var result = await _mediator.Send(new GetIngredientsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchIngredient")]
        public async Task<IActionResult> Search([FromQuery] SearchIngredientQueryRequest request)
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
