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
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetServices()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    return Ok(ServiceRepo.GetServices());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
        
        [HttpPost]
        public IActionResult CreateService([FromBody] Service service, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    if (ServiceRepo.CheckServiceName(service.Name))
                    {
                        return BadRequest(new {error = "Name already exists!"});
                    }
                    ServiceRepo.CreateService(service);
                    return Ok(ServiceRepo.GetServices());

                }
            }
            catch (Exception e)
            {

                return StatusCode(500, "Error: " + e);
            }
            
        }

        [HttpPut]
        public IActionResult EditService([FromQuery]int id, [FromBody] Service service, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    ServiceRepo.EditService(id, service);
                    return Ok(ServiceRepo.GetServices());
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete]
        public IActionResult DeleteService([FromQuery]int id, [FromQuery]string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    ServiceRepo.DeleteService(id);
                    return Ok(ServiceRepo.GetServices());
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint "))
                    return StatusCode(501, "You cannot delete this service, as it is used in one or more appointment");
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
