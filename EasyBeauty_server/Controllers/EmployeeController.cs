

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
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    return Ok(EmployeeRepo.GetEmployees());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody]Employee employee, [FromQuery]string cookie)
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
                    if (EmployeeRepo.CheckEmployeeEmail(employee.Email)) return Ok(new {error = "Email already Exists!"});
                    EmployeeRepo.CreateEmployee(employee);
                    return Ok(new { success = "Employee saved" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }

        }

        [HttpPut]
        public IActionResult EditEmployee([FromQuery]int id, [FromBody] Employee employee, [FromQuery]string cookie)
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
                    EmployeeRepo.EditEmployee(id, employee);
                    return Ok(new { success = "Employee saved" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new{error = e});
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery]int id, [FromQuery]string cookie)
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
                    EmployeeRepo.DeleteEmployee(id);
                    return Ok(new { success = "Employee deleted" });
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return StatusCode(501,
                        "You cannot delete this employee, as it has one or more booked appointments");
                }
                return StatusCode(500, new{error = e});
            }
        }
    }
}
