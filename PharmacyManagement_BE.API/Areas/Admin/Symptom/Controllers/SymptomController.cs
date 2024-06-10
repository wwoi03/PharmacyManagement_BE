using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.SymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.SymptomFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Symptom.Controllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class SymptomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SymptomController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("CreateSymptom")]
        public async Task<IActionResult> Create(CreateSymptomCommandRequest request)
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

        [HttpDelete("DeleteSymptom")]
        public async Task<IActionResult> Delete(DeleteSymptomCommandRequest request)
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

        [HttpPut("UpdateSymptom")]
        public async Task<IActionResult> Update(UpdateSymptomCommandRequest request)
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

        [HttpGet("GetSymptoms")]
        public async Task<IActionResult> GetSymptoms()
        {
            try
            {
                var result = await _mediator.Send(new GetSymptomsQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SearchSymptom")]
        public async Task<IActionResult> Search(SearchSymptomQueryRequest request)
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
