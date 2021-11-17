using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
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
        public void GetAllServices()
        {
            var result = controller.GetServices();
            Assert.IsType<List<Service>>(result);

        }

    }
}
