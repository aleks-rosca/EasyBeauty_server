using Microsoft.AspNetCore.Mvc;
using EasyBeauty_server.Models;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Repository;
using System;
using System.Collections.Generic;
using EasyBeauty_server.Helpers;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyBeauty_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> Get()
        {
            using (DBConnection.GetConnection())
            {
                var result = EmployeeRepo.GetEmployees();
                return result;
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void CreateEmployee(Employee employee)
        {
            try { 
                using (DBConnection.GetConnection())
                {
                EmployeeRepo.CreateEmployee(employee);
                }
          } catch(Exception e)
            {
                Console.Write(e.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public string Put(int id, Employee employee)
        {
            var hashedPassword = Hashing.HashString(employee.Password);
            try
            {
                using (DBConnection.GetConnection())
                {
                    EmployeeRepo.EditEmployee(id, employee);
                    
                }
                
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
            
            Console.Write(hashedPassword);
            return "password: " + hashedPassword;
           
        }



        // DELETE api/<EmployeeController>/5
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
            catch(Exception e)
            {
                Console.Write(e);
            }

        }
    }
}
