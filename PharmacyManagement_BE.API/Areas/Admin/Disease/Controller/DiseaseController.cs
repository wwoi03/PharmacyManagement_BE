using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Disease.Controller
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class DiseaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiseaseController (IMediator mediator )
        {
            this._mediator = mediator;
        }

        //Tạo bệnh mới
        [HttpPost("CreateDisease")]
        public async Task<IActionResult> Create(CreateDiseaseCommandRequest request)
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

        //Cập nhật bệnh 
        [HttpPut("UpdateDisease")]
        public async Task<IActionResult> Update(UpdateDiseaseCommandRequest request)
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

        [HttpDelete ("DeleteDisease")]
        public async Task<IActionResult> Delete(DeleteDiseaseCommandRequest request)
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

        [HttpPost("DetailsDisease")]
        public async Task<IActionResult> Details(GetDetailsDiseaseQueryRequest request)
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

        [HttpGet("GetDiseases")]
        public async Task<IActionResult> GetDiseases()
        {
            try
            {
                var result = await _mediator.Send(new GetDiseasesQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SearchDisease")]
        public async Task<IActionResult> Search(SearchDiseasesQueryRequest request)
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
