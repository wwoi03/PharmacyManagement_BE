using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;

namespace PharmacyManagement_BE.API.Areas.Admin.Upload.UploadControllers
{
    [ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("SaveFile")]
        public IActionResult SaveFile()
        {
            try
            {
                // Nhận dữ liệu từ yêu cầu HTTP
                var httpRequest = HttpContext.Request.Form;

                // Lấy ra tệp tin được gửi trong yêu cầu
                var postFile = httpRequest.Files[0];

                // Lấy ra tên của tệp tin
                string fileName = Guid.NewGuid() + "-" + postFile.FileName;

                // Tạo đường dẫn vật lý cho việc lưu trữ tệp tin trên máy chủ
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + fileName;

                // Mở một luồng để sao chép dữ liệu từ tệp tin gửi đến vào tệp tin trên máy chủ
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }

                // Trả về tên của tệp tin đã được lưu trữ thành công
                return Ok(new ResponseSuccessAPI<string>(StatusCodes.Status200OK, fileName));
            }
            catch (Exception)
            {
                // Nếu có lỗi xảy ra, trả về tên của tệp tin mặc định
                return Ok(new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Lỗi hệ thống"));
            }
        }
    }
}
