using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyManagement_BE.API.Areas.Admin.Account.Controllers;
using PharmacyManagement_BE.API.Areas.Admin.Staff.Controllers;
using PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyManagement_BE.Tests.UnitTests.Controllers.Account
{
    public class AccountControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private AccountController _controller;

        public AccountControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new AccountController(_mockMediator.Object);
        }

        [Fact]
        public async Task SignIn_ReturnsOkResult_WithValidUser()
        {
            // Arrange
            var expectedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibnYwMSIsImp0aSI6IjQ0MTQ1MjA0LWQ4NDYtNGFiOC1iY2RiLWFhMjcwNTMzZTQ3YSIsImV4cCI6MTcxODA0NzYxOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiVXNlciJ9.kYCdXzSPL43iLWVhOWSjqQ-s2p4-furGr9g8a6cfGu0";
            var expectedResult = new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Đăng nhập thành công.", expectedToken);

            // Cài đặt cho mock Mediator trả về giá trị mong đợi khi gọi phương thức Send.
            _mockMediator.Setup(x => x.Send(It.IsAny<SignInCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult); // Định nghĩa giá trị trả về là đối tượng kết quả mong đợi.

            // Act
            // Gọi phương thức cần kiểm thử trên controller với giá trị ID giả.
            var request = new SignInCommandRequest
            {
                UserName = "nv01",
                Password = ""
            };

            var result = await _controller.SignIn(request);

            // Assert
            // Kiểm tra kết quả trả về có phải là kiểu OkObjectResult hay không.
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Kiểm tra giá trị bên trong OkObjectResult có phải là kiểu ResponseSuccessAPI hay không.
            var returnValue = Assert.IsType<ResponseSuccessAPI<string>>(okResult.Value);

            // Kiểm tra xem kết quả thực tế có khớp với kết quả mong đợi hay không.
            Assert.Equal(expectedResult.Code, returnValue.Code);
            Assert.Equal(expectedResult.Obj, returnValue.Obj);
            Assert.Equal(expectedResult, returnValue);
        }

        [Fact]
        public async Task SignIn_ReturnsNotFoundResult_WhenUserNotFound()
        {
            // Arrange
            var expectedResult = new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Người dùng không tồn tại.");

            _mockMediator.Setup(x => x.Send(It.IsAny<SignInCommandRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResult); 

            var request = new SignInCommandRequest
            {
                UserName = "nv02", // Giả lập một người dùng không tồn tại
                Password = "password"
            };

            // Act
            var result = await _controller.SignIn(request);

            // Assert
            var notFoundResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseErrorAPI<string>>(notFoundResult.Value);

            Assert.Equal(expectedResult.Code, returnValue.Code);
            Assert.Equal(expectedResult.Message, returnValue.Message);
        }
    }
}
