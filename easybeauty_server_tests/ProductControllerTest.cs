using EasyBeauty_server.Controllers;
using EasyBeauty_server.Models;
using System;
using System.Collections.Generic;
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
            var result = controller.GetProducts();
            Assert.IsType<List<Product>>(result);

        }

    }
}
