using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Application.Commands.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.ProductDiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Application.Queries.ProductDiseaseFeatures.Requests;

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
        public async Task<IActionResult> Delete([FromQuery]DeleteDiseaseCommandRequest request)
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

        [HttpGet("DetailsDisease")]
        public async Task<IActionResult> Details([FromQuery]GetDetailsDiseaseQueryRequest request)
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

        [HttpGet("SearchDisease")]
        public async Task<IActionResult> Search([FromQuery]SearchDiseaseQueryRequest request)
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

        // quan hệ DiseaseProducts
        [HttpPost("CreateProductDisease")]
        public async Task<IActionResult> CreateProductDisease(CreateProductDiseaseCommandRequest request)
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
        [HttpDelete("DeleteProductDisease")]
        public async Task<IActionResult> DeleteProductDisease([FromQuery] DeleteProductDiseaseCommandRequest request)
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
        [HttpGet("GetProductDiseases")]
        public async Task<IActionResult> GetProductDiseases([FromQuery] GetDiseaseProductsQueryRequest request)
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
        // quan hệ Disease symptom
        [HttpPost("CreateDiseaseSymptom")]
        public async Task<IActionResult> CreateDiseaseSymptom(CreateDiseaseSymptomCommandRequest request)
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
        [HttpDelete("DeleteDiseaseSymptom")]
        public async Task<IActionResult> DeleteDiseaseSymptom([FromQuery] DeleteDiseaseSymptomCommandRequest request)
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
        [HttpGet("GetDiseaseSymptoms")]
        public async Task<IActionResult> GetDiseaseSymptoms([FromQuery] GetDiseaseSymptomsQueryRequest request)
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
