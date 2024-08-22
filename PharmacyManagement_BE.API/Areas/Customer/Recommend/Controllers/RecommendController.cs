using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Queries.ProductPredictionFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Customer.Recommend.Controllers
{
    [ApiExplorerSettings(GroupName = "Customer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class RecommendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecommendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetSimilarProducts")]
        public async Task<IActionResult> GetSimilarProducts()
        {
            try
            {
                var result = await _mediator.Send(new GetSimilarProductsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
