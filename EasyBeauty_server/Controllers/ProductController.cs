using EasyBeauty_server.Helpers;

namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts([FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    return LoginRepo.CheckLogin(user.Id) ? Ok(ProductRepo.GetProducts()) : StatusCode(401, "Not Logged In");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
        
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
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

        [HttpPut]
        public IActionResult EditProduct([FromQuery]int id, [FromBody] Product product, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    ProductRepo.EditProduct(id, product);
                    return Ok(ProductRepo.GetProducts());
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromQuery]int id, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
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
