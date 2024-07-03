using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.CategoryFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.CategoryFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Category.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
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

        [HttpGet("GetParentCategories")]
        public async Task<IActionResult> GetParentCategories()
        {
            try
            {
                var result = await _mediator.Send(new GetParentCategoriesQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChildrenCategories")]
        public async Task<IActionResult> GetChildrenCategories([FromQuery] Guid parentCategoryId)
        {
            try
            {
                var result = await _mediator.Send(new GetChildrenCategoriesQueryRequest { ParentCategoryId = parentCategoryId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchCategories")]
        public async Task<IActionResult> SearchCategories([FromQuery] string contentStr)
        {
            try
            {
                var result = await _mediator.Send(new SearchCategoriesQueryRequest { ContentStr = contentStr });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHierarchicalCategories")]
        public async Task<IActionResult> GetHierarchicalCategories()
        {
            try
            {
                var result = await _mediator.Send(new GetHierarchicalCategoriesQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCategoriesByLevel")]
        public async Task<IActionResult> GetCategoriesByLevel()
        {
            try
            {
                var result = await _mediator.Send(new GetCategoriesByLevelQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommandRequest request)
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommandRequest request)
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid categoryId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCategoryCommandRequest { CategoryId = categoryId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details([FromQuery] Guid categoryId)
        {
            try
            {
                var result = await _mediator.Send(new GetCategoryDetailsQueryRequest { CategoryId = categoryId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
