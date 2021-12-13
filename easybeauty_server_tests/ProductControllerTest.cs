using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
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

         [Fact]
         public void CreateProduct()
         {
             var prd = new Product()
             {
                 Name = "xUnitTest",
                 Description = "test from xunit",
                 Image = "testurl",
                 Price = 200
             };
             var result = controller.CreateProduct(prd,
                 "eyJJZCI6MSwiTmFtZSI6IkFsZXhhbmRydSBSb3NjYSIsIlJvbGUiOiJtYW5hZ2VyIiwiVG9rZW4iOiIxQTI5QTgyNDMyNjkxMzMzMTQ1QTM4RDI0MjlBOEYyQTI5NjI1QzdFNjAzQjk3MTVCOTUyNkM3MkRBMDFCRjI4In0");
             var okResult = result as OkObjectResult;
             Assert.NotNull(result);
             Assert.Equal(200, okResult?.StatusCode);
         }
    }
}
