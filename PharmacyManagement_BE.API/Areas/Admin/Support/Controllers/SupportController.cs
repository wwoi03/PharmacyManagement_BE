using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.ProductSupportFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Support.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class SupportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupportController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("CreateSupport")]
        public async Task<IActionResult> Create(CreateSupportCommandRequest request)
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

        [HttpDelete("DeleteSupport")]
        public async Task<IActionResult> Delete([FromQuery] DeleteSupportCommandRequest request)
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

        [HttpPut("UpdateSupport")]
        public async Task<IActionResult> Update(UpdateSupportCommandRequest request)
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

        [HttpGet("DetailsSupport")]
        public async Task<IActionResult> Details([FromQuery] GetDetailsSupportQueryRequest request)
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

        [HttpGet("GetSupports")]
        public async Task<IActionResult> GetSupports()
        {
            try
            {
                var result = await _mediator.Send(new GetSupportsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchSupport")]
        public async Task<IActionResult> Search([FromQuery] SearchSupportQueryRequest request)
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

        // quan hệ Product support
        [HttpPost("CreateProductSupport")]
        public async Task<IActionResult> CreateProductSupport(CreateProductSupportCommandRequest request)
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
        [HttpDelete("DeleteProductSupport")]
        public async Task<IActionResult> DeleteProductSupport([FromQuery] DeleteProductSupportCommandRequest request)
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
        [HttpGet("GetProductSupports")]
        public async Task<IActionResult> GetSupportProducts([FromQuery] GetSupportProductsQueryRequest request)
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

        [HttpGet("GetSupportSelect")]
        public async Task<IActionResult> GetSupportSelect()
        {
            try
            {
                var result = await _mediator.Send(new GetSupportSelectQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
