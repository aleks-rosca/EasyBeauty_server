using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace easybeauty_server_tests
{
    public class EmployeeControllerTest
    {
        readonly EmployeeController controller;

        public EmployeeControllerTest()
        {
            controller = new EmployeeController();
        }
        [Fact]
        public void GetAllEmployees()
        {
            var result = controller.GetEmployees();
            Assert.IsType<List<IActionResult>>(result);

        }
            
    }
}
