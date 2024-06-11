using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PharmacyManagement_BE.API;
using PharmacyManagement_BE.Tests.Fatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PharmacyManagement_BE.Tests.IntegrationTests.Controllers.ShipmentDetails
{
    public class ShipmentDetailsControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact]
        public async Task Test1() // Định nghĩa phương thức kiểm thử đơn vị.
        {
            try
            {
                var application = new PMWebApplicationFactory();
                var client = application.CreateClient();

                var shipmentDetailsId = new Guid("B6D0AC2B-7E25-437E-BEC0-02847ADB06E3");
                var content = new StringContent($"\"{shipmentDetailsId}\"", Encoding.UTF8, "application/json"); // Đóng gói Guid dưới dạng chuỗi JSON
                var response = await client.PostAsync("/api/admin/shipmentdetails/GetDetailsShipmentDetails", content);

                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData);
            }
            catch (InvalidOperationException ex)
            {
                // Ghi log lỗi
                Console.WriteLine(ex.Message);
            }
        }
    }
}
