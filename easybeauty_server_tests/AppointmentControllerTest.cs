using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace easybeauty_server_tests
{
    public class AppointmentControllerTest
    {
        readonly AppointmentController controller;

        public AppointmentControllerTest()
        {
            controller = new AppointmentController();
        }
        
        
        [Fact]
        public void GetAppointmentByEmployee()
        {
            var result = controller.GetAppointmentsByEmployee(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }
        [Fact]
        public void GetEMployeeTimeSchedule()
        {
            var result = controller.GetEmployeeTimeSchedule(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
