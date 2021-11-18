namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http.Headers;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {

            try
            {
                using (DBConnection.GetConnection())
                {
                   // if (!LoginRepo.CheckToken(employeeId, token)) { throw new ArgumentException("Not Logged In"); }
                    return Ok(ProductRepo.GetProducts());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (ProductRepo.CheckProductName(product.Name))
                    {
                        return BadRequest(new { error = "Name already exists!" });
                    }
                    ProductRepo.CreateProduct(product);
                    return Ok(ProductRepo.GetProducts());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditProduct(int id, [FromBody] Product product)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ProductRepo.EditProduct(id, product);
                    return Ok(ProductRepo.GetProducts());

                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ProductRepo.DeleteProduct(id);
                    return Ok(ProductRepo.GetProducts());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
