using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Domain.Roles;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;

namespace PharmacyManagement_BE.API.Areas.Customer.Product.Controllers
{
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

        [HttpGet("GetProducts")]
        [Authorize()]
        public async Task<IActionResult> Get()
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
    }
}
