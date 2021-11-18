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

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult CreateService([FromBody] Service service)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
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

        [HttpPut("{id}")]
        public IActionResult EditService(int id, [FromBody] Service service)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ServiceRepo.EditService(id, service);
                    return Ok(ServiceRepo.GetServices());
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            try
            {
                using (DBConnection.GetConnection())
                {
                    ServiceRepo.DeleteService(id);
                    return Ok(ServiceRepo.GetServices());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error: " + e);
            }
        }
    }
}
