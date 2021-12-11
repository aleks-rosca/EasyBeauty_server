using EasyBeauty_server.Controllers;

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
        public void GetEmployeeTimeSchedule()
        {
            var result = controller.GetEmployeeTimeSchedule(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void CheckCustomer()
        {
            var result = controller.CheckCustomer(12345678);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
