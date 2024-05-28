using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;

namespace PharmacyManagement_BE.API.Areas.Admin.Disease.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin/Product")]
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
        [HttpPost("UpdateDisease")]
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
        public async Task<IActionResult> Details(GetDetailsDiseaseCommandRequest request)
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

        [HttpPost("ListDisease")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _mediator.Send(new GetAllDiseaseCommandRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SearchDisease")]
        public async Task<IActionResult> Search(SearchDiseaseCommandRequest request)
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
