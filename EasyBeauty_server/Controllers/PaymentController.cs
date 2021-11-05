using EasyBeauty_server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EasyBeauty_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpPost]
        public object Post([FromBody] Payment amount)
        {
            var random = new Random();
            var list = new List<object> { new { succes = "Payment Successfull" }, new { error = "Incorrect Pin Code" }, new { error = "Insuficient Funds" }, new { error = "Unknown error"}, new { error = "Something went wrong" } };
            int i = random.Next(list.Count);
            return list[i];
        }

    }
}
