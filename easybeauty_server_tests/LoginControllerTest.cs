using EasyBeauty_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Sdk;

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
            var result = controller.CheckEmail("us.account.blizzard@gmail.com1");
            var positiveResult = controller.CheckEmail("us.account.blizzard@gmail.com");
            var okResult = positiveResult as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult?.StatusCode);
            Assert.IsNotType<OkObjectResult>(result);
        }

        public void Login()
        {
            var login = controller.Login("us.account.blizzard@gmail.com", "aleks4444");
            
        }
    }
}
