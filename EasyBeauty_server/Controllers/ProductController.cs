namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public List<Product> GetProducts()
        {

            try
            {
                using (DBConnection.GetConnection())
                {
                   // if (!LoginRepo.CheckToken(employeeId, token)) { throw new ArgumentException("Not Logged In"); }
                    var result = ProductRepo.GetProducts();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<Product>();
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public string CreateProduct([FromBody] Product product)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (ProductRepo.CheckProductName(product.Name))
                    {
                        return "Product name already exists!";
                    }
                    ProductRepo.CreateProduct(product);

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return "";
        }

        [HttpPut("{id}")]
        public void EditProduct(int id, [FromBody] Product product)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ProductRepo.EditProduct(id, product);

                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ProductRepo.DeleteProduct(id);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
