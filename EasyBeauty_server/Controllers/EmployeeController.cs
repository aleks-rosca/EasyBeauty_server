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
    public class EmployeeController : ControllerBase
    {
        [HttpGet("{cookie}")]
        public IActionResult GetEmployees(string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    return !LoginRepo.CheckLogin(user.Id) ? StatusCode(401, "Not Logged in") : Ok(EmployeeRepo.GetEmployees());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpPost("{cookie}")]
        public IActionResult CreateEmployee(Employee employee, string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    if (EmployeeRepo.CheckEmployeeEmail(employee.Email))
                    {
                        return Ok(new {error = "Email already Exists!" });
                    }
                    EmployeeRepo.CreateEmployee(employee);
                    return Ok(new { success = "Employee saved" });

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }

        }

        [HttpPut("{id},{cookie}")]
        public IActionResult EditEmployee(int id, [FromBody] Employee employee, string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
                    EmployeeRepo.EditEmployee(id, employee);
                    return Ok(new { success = "Employee saved" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("{id},{cookie}")]
        public IActionResult DeleteEmployee(int id, string cookie)
        {
            var user = CookieEncDec.DecryptCookie(cookie);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (!LoginRepo.CheckLogin(user.Id)) return StatusCode(401, "Not Logged in");
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
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
