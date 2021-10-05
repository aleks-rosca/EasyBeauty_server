using EasyBeauty_server.DataAccess;
using EasyBeauty_server.Models;
using EasyBeauty_server.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyBeauty_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        // GET: api/<MainController>
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            using (DBConnection.GetConnection())
            {
                var result = EmployeeRepo.GetEmployees();
                DBConnection.CloseConnection();
                return result;
                
            }
            

        }
           

        // GET api/<MainController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MainController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MainController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
