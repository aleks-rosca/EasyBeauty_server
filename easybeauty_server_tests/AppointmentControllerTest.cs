using System;
using System.Collections.Generic;
using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using EasyBeauty_server.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace easybeauty_server_tests
{
    public class AppointmentControllerTest
    {
        readonly AppointmentController _controller;
        private readonly ITestOutputHelper _output;

        public AppointmentControllerTest(ITestOutputHelper output)
        {
            _controller = new AppointmentController();
            this._output = output;
        }

        
        
        [Fact]
        public void GetAppointmentByEmployee()
        {
            var result = _controller.GetAppointmentsByEmployee(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }
        [Fact]
        public void GetEmployeeTimeSchedule()
        {
            var result = _controller.GetEmployeeTimeSchedule(1);
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        // [Fact]
        // public void CheckCustomer()
        // {
        //     var result = _controller.CheckCustomer(12345678);
        //     var okResult = result as OkObjectResult;
        //     Assert.NotNull(result);
        //     Assert.Equal(200, okResult?.StatusCode);
        // }
        [Fact]
        public void CreateAppointment()
        {
            var appointment = new Appointment
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromHours(1),
                EmployeeId = 1,
                ServiceId = 20,
                CustomerName = "xUnit Test",
                PhoneNr = 12345678,
                CustomerEmail = "xUnit@test.com",
                Notes = "test"
            };
            
            var actionResult = _controller.CreateAppointment(appointment);
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var actual = _controller.CheckCustomer(12345678);
            var res = actual as OkObjectResult;
            Assert.NotNull(actual);
            if (res != null) Assert.IsType<Customer>(res.Value);
        }
    }
    
}
