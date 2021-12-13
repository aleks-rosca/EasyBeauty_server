using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
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
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public void CreateEmployee()
        {
            var emp = new Employee()
            {
                Email = "xtest@test.com",
                Name = " xTest unit",
                PhoneNr = 23456789,
                Role = "employee"
            };
            var result = controller.CreateEmployee(emp,"eyJJZCI6MSwiTmFtZSI6IkFsZXhhbmRydSBSb3NjYSIsIlJvbGUiOiJtYW5hZ2VyIiwiVG9rZW4iOiIxQTI5QTgyNDMyNjkxMzMzMTQ1QTM4RDI0MjlBOEYyQTI5NjI1QzdFNjAzQjk3MTVCOTUyNkM3MkRBMDFCRjI4In0");
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
            
        }
            
    }
}
