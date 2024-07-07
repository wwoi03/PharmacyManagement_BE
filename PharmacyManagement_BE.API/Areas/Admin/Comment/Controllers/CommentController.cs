using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.CommentFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.SupportFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Comment.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("CreateReplyComment")]
        public async Task<IActionResult> CreateReply(CreateReplyCommentCommandRequest request)
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


        [HttpGet("DetailsComment")]
        public async Task<IActionResult> Details([FromQuery]GetDetailsCommentQueryRequest request)
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
