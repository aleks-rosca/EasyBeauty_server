using EasyBeauty_server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EasyBeauty_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Payment amount)
        {
            var random = new Random();
            var list = new List<object> { new { succes = "Payment Successful" }, new { error = "Incorrect Pin Code" }, new { error = "Insufficient Funds" }, new { error = "Unknown error"}, new { error = "Something went wrong" } };
            var i = random.Next(list.Count);
            return Ok(list[i]);
        }

    }
}
