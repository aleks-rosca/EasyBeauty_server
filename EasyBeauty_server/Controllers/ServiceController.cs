
namespace EasyBeauty_server.Controllers
{
    using DataAccess;
    using Models;
    using Repository;
    using Helpers;
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
                return StatusCode(500, new{error = e});
            }
        }
        
        [HttpPost]
        public IActionResult CreateService([FromBody] Service service, [FromQuery]string cookie)
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

                return StatusCode(500,new{error = e});
            }
            
        }

        [HttpPut]
        public IActionResult EditService([FromQuery]int id, [FromBody] Service service, [FromQuery]string cookie)
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
                    ServiceRepo.EditService(id, service);
                    return Ok(ServiceRepo.GetServices());
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpDelete]
        public IActionResult DeleteService([FromQuery]int id, [FromQuery]string cookie)
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
                    ServiceRepo.DeleteService(id);
                    return Ok(ServiceRepo.GetServices());
                }
            }
            catch (Exception e)
            {
                return e.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint ") ? StatusCode(501, "You cannot delete this service, as it is used in one or more appointment") : StatusCode(500, new{error = e});
            }
        }
    }
}
