using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBeauty_server.Models
{
    public class Service
    {
        public int ServiceID { set; get; }
        public string ServiceName { set; get; }
        public string ServiceDescription { set; get; }
        public double ServicePrice { set; get; }
        public byte[] ServiceImage { set; get; }
        public int Duration { set; get; }
    }
}
