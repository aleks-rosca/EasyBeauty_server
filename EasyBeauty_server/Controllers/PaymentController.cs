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
        public string Post([FromBody] int amount)
        {
            var random = new Random();
            var list = new List<string> { "Payment Successfull", "Incorrect Pin Code", "Insuficient Funds" };
            int i = random.Next(list.Count);
            return list[i];
        }

    }
}
