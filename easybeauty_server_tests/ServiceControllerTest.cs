using Dapper;
using EasyBeauty_server.Controllers;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using EasyBeauty_server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
