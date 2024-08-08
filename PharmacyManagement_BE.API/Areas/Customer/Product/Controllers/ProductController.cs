using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Application.Queries.ProductEcommerceFeatures.Requests;
using PharmacyManagement_BE.Domain.Roles;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.Securitys;

namespace PharmacyManagement_BE.API.Areas.Customer.Product.Controllers
{
    [ApiExplorerSettings(GroupName = "Customer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetProductById")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                //var result = await _mediator.Send(new GetAllProductQueryRequest());
                return Ok("Thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSellingProductByMonth")]
        public async Task<IActionResult> GetSellingProductByMonth()
        {
            try
            {
                var result = await _mediator.Send(new GetSellingProductByMonthQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetNewProducts")]
        public async Task<IActionResult> GetNewProducts()
        {
            try
            {
                var result = await _mediator.Send(new GetNewProductsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSaleProducts")]
        public async Task<IActionResult> GetSaleProducts()
        {
            try
            {
                var result = await _mediator.Send(new GetSaleProductsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductDetails")]
        public async Task<IActionResult> GetProductDetails([FromQuery] Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new GetProductDetailsQueryRequest { ProductId = productId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
