using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PharmacyManagement_BE.API.Areas.Admin.ShipmentDetails.Controllers;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyManagement_BE.Tests.UnitTests.Controllers.ShipmentDetails
{
    public class ShipmentDetailsControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private ShipmentDetailsController _controller;

        public ShipmentDetailsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new ShipmentDetailsController(_mockMediator.Object);
        }

        [Fact]
        public async Task Test1() // Định nghĩa phương thức kiểm thử đơn vị.
        {
            // Arrange
            var shipmentDetailsId = new Guid("B6D0AC2B-7E25-437E-BEC0-02847ADB06E3");
            var expectedDetailsShipmentDetailsDTO = new DetailsShipmentDetailsDTO
            {
                ShipmentDetailsId = shipmentDetailsId,
                SupplierName = "Trupe AA",
                ProductId = new Guid("2460CB8F-2DDD-4164-97E9-CDBF16700852"),
                ProductName = "Bacardi Raspberry",
                ProductImage = "",
                ImportPrice = 100000,
                Quantity = 1000,
                Sold = 0,
                ManufactureDate = DateTime.ParseExact("2020-06-09T09:48:20.431", "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture),
                ExpirationDate = DateTime.ParseExact("2024-06-09T09:48:20.431", "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture),
                Note = "",
                ProductionBatch = "57520-0382",
                AdditionalInfo = null
            };

            var expectedResult = new ResponseSuccessAPI<DetailsShipmentDetailsDTO>(StatusCodes.Status200OK, expectedDetailsShipmentDetailsDTO);

            // Cài đặt cho mock Mediator trả về giá trị mong đợi khi gọi phương thức Send.
            _mockMediator.Setup(x => x.Send(It.IsAny<GetDetailsShipmentDetailsQueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult); // Định nghĩa giá trị trả về là đối tượng kết quả mong đợi.

            // Act
            // Gọi phương thức cần kiểm thử trên controller với giá trị ID giả.
            var result = await _controller.GetDetailsShipmentDetails(shipmentDetailsId);

            // Assert
            // Kiểm tra kết quả trả về có phải là kiểu OkObjectResult hay không.
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Kiểm tra giá trị bên trong OkObjectResult có phải là kiểu ResponseSuccessAPI hay không.
            var returnValue = Assert.IsType<ResponseSuccessAPI<DetailsShipmentDetailsDTO>>(okResult.Value);

            // Kiểm tra xem kết quả thực tế có khớp với kết quả mong đợi hay không.
            Assert.Equal(expectedResult, returnValue);
        }
    }
}
