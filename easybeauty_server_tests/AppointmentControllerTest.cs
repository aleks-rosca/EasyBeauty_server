using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
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
            var result = controller.GetAppointmentsByEmployee(1,"");
            Assert.IsType<List<AppointmentDB>>(result);
        }
        [Fact]
        public void GetEMployeeTimeSchedule()
        {
            var result = controller.GetEmployeeTimeSchedule(1);
            Assert.IsType<List<EmployeeSchedule>>(result);
        }
    }
}
