using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.ProductFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;

namespace PharmacyManagement_BE.API.Areas.Admin.Product.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommandRequest { ProductId = productId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("SearchProduct")]
        public async Task<IActionResult> SearchProduct(Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommandRequest { ProductId = productId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
