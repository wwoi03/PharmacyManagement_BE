using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PharmacyManagement_BE.API;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Tests.Fatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyManagement_BE.Tests.IntegrationTests.ShipmentDetails
{
    public class ShipmentDetailsControllerTest
    {
        private readonly HttpClient _client;

        public ShipmentDetailsControllerTest()
        {
            _client = new PMWebApplicationFactory().CreateClient();
        }

        [Fact]
        public async Task GetShipmentDetails_ReturnsNotFoundError()
        {
            try
            {
                // Arrange
                var shipmentDetailsId = new Guid("B6D0AC2B-7E25-437E-BEC0-02847ADB06E3");

                // Act
                var response = await _client.GetAsync($"/api/admin/shipmentdetails/GetDetailsShipmentDetails?shipmentDetailsId={shipmentDetailsId}");

                var expectedResult = new ResponseErrorAPI<DetailsShipmentDetailsDTO>
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Chi tiết đơn hàng không tồn tại."
                };
                var actualResult = await response.Content.ReadFromJsonAsync<ResponseErrorAPI<DetailsShipmentDetailsDTO>>();

                // Assert
                Assert.NotNull(actualResult);
                Assert.Equal(expectedResult.Code, actualResult.Code);
                Assert.Equal(expectedResult.Message, actualResult.Message);
                Assert.Equal(expectedResult.Obj, actualResult.Obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
