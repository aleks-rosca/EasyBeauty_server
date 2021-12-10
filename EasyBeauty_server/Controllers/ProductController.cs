namespace EasyBeauty_server.Controllers
{
    using DataAccess;
    using Models;
    using Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using EasyBeauty_server.Helpers;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts([FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Token);
                        return Ok(new { error = "Not logged in" });
                    }
                    return Ok(ProductRepo.GetProducts());
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                return StatusCode(500, new{error = e});
                
            }
        }
        
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product, [FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Token);
                        return Ok(new { error = "Not logged in" });
                    }
                    if (!EmployeeRepo.GetRole(user.Id).Equals("manager")) return StatusCode(402,new {error = "Wrong Privileges"});
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
                return StatusCode(500, new{error = e});
            }
        }

        [HttpPut]
        public IActionResult EditProduct([FromQuery]int id, [FromBody] Product product, [FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Token);
                        return Ok(new { error = "Not logged in" });
                    }
                    if (!EmployeeRepo.GetRole(user.Id).Equals("manager")) return StatusCode(402,new {error = "Wrong Privileges"});
                    ProductRepo.EditProduct(id, product);
                    return Ok(ProductRepo.GetProducts());
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromQuery]int id, [FromQuery]string cookie)
        {
            if (string.IsNullOrEmpty(cookie)) return BadRequest(new {error = "internal error"});
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckToken(user.Id, user.Token))
                    {
                        LoginRepo.RemoveToken(user.Token);
                        return Ok(new { error = "Not logged in" });
                    }
                    if (!EmployeeRepo.GetRole(user.Id).Equals("manager")) return StatusCode(402,new {error = "Wrong Privileges"});
                    ProductRepo.DeleteProduct(id);
                    return Ok(ProductRepo.GetProducts());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }
    }
}
