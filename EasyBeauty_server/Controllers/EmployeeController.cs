using Microsoft.AspNetCore.Mvc;
using EasyBeauty_server.Models;
using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                DBConnection.CloseConnection();
                return result;
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void CreateEmployee(Employee employee)
        {
            try { 
            using (DBConnection.GetConnection())
            {
                EmployeeRepo.CreateEmployee(employee);
                DBConnection.CloseConnection();
            }
          } catch(Exception e)
            {
                Console.Write(e.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
