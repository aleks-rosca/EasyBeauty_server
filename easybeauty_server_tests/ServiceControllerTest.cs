using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
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
        [Fact]
        public void CreateService()
        {
            var srv = new Service()
            {
                Name = "xUnitTest",
                Description = "test from xunit",
                Image = "testurl",
                Price = 200,
                Duration = 120
            };
            var result = controller.CreateService(srv,
                "eyJJZCI6MSwiTmFtZSI6IkFsZXhhbmRydSBSb3NjYSIsIlJvbGUiOiJtYW5hZ2VyIiwiVG9rZW4iOiIxQTI5QTgyNDMyNjkxMzMzMTQ1QTM4RDI0MjlBOEYyQTI5NjI1QzdFNjAzQjk3MTVCOTUyNkM3MkRBMDFCRjI4In0");
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

    }
}
