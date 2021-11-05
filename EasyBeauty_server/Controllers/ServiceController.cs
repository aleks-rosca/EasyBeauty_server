namespace EasyBeauty_server.Controllers
{
    using EasyBeauty_server.DataAccess;
    using EasyBeauty_server.Models;
    using EasyBeauty_server.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using static System.Net.Mime.MediaTypeNames;

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public List<Service> GetServices()
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    var result = ServiceRepo.GetServices();
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<Service>();
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CreateService([FromBody] Service service)
        {
            Console.WriteLine(service);
            try
            {
                using (DBConnection.GetConnection())
                {
                    if (ServiceRepo.CheckServiceName(service.Name))
                    {
                        return BadRequest("Service name already exists!");
                    }
                    ServiceRepo.CreateService(service);
                    return Ok("Service Created");

                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
        }

        [HttpPut("{id}")]
        public void EditService(int id, [FromBody] Service service)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ServiceRepo.EditService(id, service);

                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteService(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ServiceRepo.DeleteService(id);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
