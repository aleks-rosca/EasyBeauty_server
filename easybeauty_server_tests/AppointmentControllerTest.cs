using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace easybeauty_server_tests
{
    public class AppointmentControllerTest
    {
        AppointmentController _controller;

        public AppointmentControllerTest()
        {
            _controller = new AppointmentController();
        }
        
        [Fact]
        public void GetAllTests()
        {
            var result = _controller.GetAppointments();
            Assert.IsType<List<Appointment>>(result);

        }
    }
}
