using EasyBeauty_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace easybeauty_server_tests
{
    public class ProductControllerTest
    {
        readonly ProductController controller;

        public ProductControllerTest()
        {
            controller = new ProductController();
        }
         [Fact]
         public void GetAllProducts()
         {
             var result = controller.GetProducts("");
             var okResult = result as OkObjectResult;
             Assert.NotNull(result);
             Assert.NotEqual(200, okResult?.StatusCode);
             Assert.IsType<BadRequestObjectResult>(result);

         }

    }
}
