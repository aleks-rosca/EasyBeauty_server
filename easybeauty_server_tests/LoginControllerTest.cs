using System;
using EasyBeauty_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace easybeauty_server_tests
{
    public class LoginControllerTest
    {
        readonly LoginController controller;
        private readonly ITestOutputHelper _output;

        public LoginControllerTest(ITestOutputHelper output)
        {
            controller = new LoginController();
            this._output = output;
        }
        [Fact]
        public void CheckEmail()
        {
            var result = controller.CheckEmail("us.account.blizzard@gmail.com");
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.Contains("hasLogin",okResult.Value.ToString() ?? string.Empty);
        }
        // [Fact]
        // public void Login()
        // {
        //     var login = controller.Login("us.account.blizzard@gmail.com", "aleks4444");
        //     var okResult = login as OkObjectResult;
        //     Assert.Equal(200, okResult?.StatusCode);
        //     Assert.NotNull(okResult);
        //     Assert.Contains("cookie",okResult.Value.ToString() ?? string.Empty);
        // }
    }
}
