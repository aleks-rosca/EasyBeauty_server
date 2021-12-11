using EasyBeauty_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace easybeauty_server_tests
{
    public class ServiceControllerTest
    {
        readonly ServiceController controller;

        public ServiceControllerTest()
        {
            controller = new ServiceController();
        }
        [Fact]
        public  void GetAllServices()
        {
            var result = controller.GetServices();
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

    }
}
