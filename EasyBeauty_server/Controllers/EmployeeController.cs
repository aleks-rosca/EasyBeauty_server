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
        public List<Employee> Get()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = EmployeeRepo.GetEmployees();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<Employee>();
            }
        }

        [HttpPost]
        public string CreateEmployee(Employee employee)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (EmployeeRepo.CheckEmployeeEmail(employee.Email)) 
                    {
                        return "Email already Exists!";
                    }
                    EmployeeRepo.CreateEmployee(employee);
                    
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return "";
        }

        [HttpPut("{id}")]
        public void Put(int id, Employee employee)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    EmployeeRepo.EditEmployee(id, employee);

                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    EmployeeRepo.DeleteEmployee(id);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
