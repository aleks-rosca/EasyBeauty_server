namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

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
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee employee)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
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

        [HttpPut("{id}")]
        public IActionResult EditEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    EmployeeRepo.EditEmployee(id, employee);
                    return Ok(new { success = "Employee saved" });
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    EmployeeRepo.DeleteEmployee(id);
                    return Ok(new { success = "Employee deleted" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
