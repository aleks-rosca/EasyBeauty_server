using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace easybeauty_server_tests
{
    public class LoginControllerTest
    {
        readonly LoginController controller;

        public LoginControllerTest()
        {
            controller = new LoginController();
        }
        [Fact]
        public void CheckEmail()
        {
            var result = controller.CheckEmail("us.account.blizzard@gmail.com");
            

            Assert.IsNotType<Object>(result);
        }
    }
}
